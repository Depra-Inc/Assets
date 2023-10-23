// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Depra.Assets.Delegates;
using Depra.Assets.Exceptions;
using Depra.Assets.Idents;
using Depra.Assets.ValueObjects;

namespace Depra.Assets.Files
{
	public sealed class AssetGroup<TAsset> :
		ILoadableAsset<IEnumerable<TAsset>>,
		IEnumerable<ILoadableAsset<TAsset>>,
		IDisposable
	{
		private readonly List<TAsset> _loadedAssets;
		private readonly IList<ILoadableAsset<TAsset>> _children;

		public AssetGroup(AssetName name, IList<ILoadableAsset<TAsset>> children = null)
		{
			Guard.AgainstNull(name, () => new ArgumentNullException(nameof(name)));

			Ident = name;
			_children = children ?? new List<ILoadableAsset<TAsset>>();
			_loadedAssets = new List<TAsset>(_children.Count);
			Size = _children.SizeForAll();
		}

		public string Name => Ident.Uri;
		public IAssetIdent Ident { get; }
		public FileSize Size { get; private set; }
		public bool IsLoaded => Children.All(asset => asset.IsLoaded);
		public IEnumerable<ILoadableAsset<TAsset>> Children => _children;

		public void AddAsset(ILoadableAsset<TAsset> asset)
		{
			Guard.AgainstNull(asset, () => new ArgumentNullException(nameof(asset)));
			Guard.AgainstAlreadyContains(asset, _children, () => new AssetAlreadyAddedToGroup(Name));

			_children.Add(asset);
		}

		public IEnumerable<TAsset> Load()
		{
			foreach (var asset in Children)
			{
				if (asset.IsLoaded)
				{
					continue;
				}

				var loadedAsset = asset.Load();
				Guard.AgainstNull(loadedAsset, () => new AssetCannotBeLoadedFromGroup(asset, Name));
				Guard.AgainstAlreadyContains(loadedAsset, @in: _loadedAssets, () => new AssetAlreadyLoaded(Name));

				_loadedAssets.Add(loadedAsset);
			}

			Size = Children.SizeForAll();
			return _loadedAssets;
		}

		public async Task<IEnumerable<TAsset>> LoadAsync(DownloadProgressDelegate onProgress = null,
			CancellationToken cancellationToken = default)
		{
			if (IsLoaded)
			{
				onProgress?.Invoke(DownloadProgress.Full);
				return _loadedAssets;
			}

			await Task.WhenAll(Children.Select(asset => LoadAssetAsync(asset, cancellationToken)));
			OnProgressChanged();
			Size = Children.SizeForAll();

			return _loadedAssets;

			async Task LoadAssetAsync(ILoadableAsset<TAsset> asset, CancellationToken token)
			{
				var loadedAsset = await asset.LoadAsync(cancellationToken: token);
				OnProgressChanged();

				Guard.AgainstNull(loadedAsset, () => new AssetCannotBeLoadedFromGroup(asset, Name));
				Guard.AgainstAlreadyContains(loadedAsset, @in: _loadedAssets, () => new AssetAlreadyLoaded(Name));

				_loadedAssets.Add(loadedAsset);
			}

			void OnProgressChanged()
			{
				var progressValue = (float) _loadedAssets.Count / _children.Count;
				var progress = new DownloadProgress(progressValue);
				onProgress?.Invoke(progress);
			}
		}

		public void Unload()
		{
			_loadedAssets.Clear();
			foreach (var asset in Children)
			{
				asset.Unload();
			}
		}

		public IEnumerator<ILoadableAsset<TAsset>> GetEnumerator() => Children.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		void IDisposable.Dispose()
		{
			Unload();
			_children.Clear();
		}
	}
}