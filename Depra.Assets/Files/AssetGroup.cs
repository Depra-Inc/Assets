// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Depra.Threading;

namespace Depra.Assets
{
	public sealed class AssetGroup<TAsset> : IAssetFile<IEnumerable<TAsset>>,
		IEnumerable<IAssetFile<TAsset>>,
		IDisposable
	{
		private readonly List<TAsset> _loadedAssets;
		private readonly IList<IAssetFile<TAsset>> _children;

		public AssetGroup(AssetName name, IList<IAssetFile<TAsset>> children = null)
		{
			Guard.AgainstNull(name, () => new ArgumentNullException(nameof(name)));

			_children = children ?? new List<IAssetFile<TAsset>>();
			_loadedAssets = new List<TAsset>(_children.Count);
			Metadata = new AssetMetadata(name, _children.Size());
		}

		public AssetMetadata Metadata { get; }
		public string Name => Metadata.Uri.Relative;
		public bool IsLoaded => Children.All(asset => asset.IsLoaded);
		public IEnumerable<IAssetFile<TAsset>> Children => _children;

		public void AddAsset(IAssetFile<TAsset> asset)
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

			Metadata.Size = Children.Size();

			return _loadedAssets;
		}

		public async ITask<IEnumerable<TAsset>> LoadAsync(DownloadProgressDelegate onProgress = null,
			CancellationToken cancellation = default)
		{
			if (IsLoaded)
			{
				onProgress?.Invoke(DownloadProgress.Full);
				return _loadedAssets;
			}

			await Task.WhenAll(Children.Select(asset => LoadAssetAsync(asset, cancellation)));

			OnProgressChanged();
			Metadata.Size = Children.Size();

			return _loadedAssets;

			async Task LoadAssetAsync(IAssetFile<TAsset> asset, CancellationToken token)
			{
				var loadedAsset = await asset.LoadAsync(cancellation: token);
				OnProgressChanged();

				Guard.AgainstNull(loadedAsset, () => new AssetCannotBeLoadedFromGroup(asset, Name));
				Guard.AgainstAlreadyContains(loadedAsset, @in: _loadedAssets, () => new AssetAlreadyLoaded(Name));

				_loadedAssets.Add(loadedAsset);
			}

			void OnProgressChanged()
			{
				var progressValue = (float)_children.Count / _loadedAssets.Count;
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

		public IEnumerable<IAssetUri> Dependencies() => _children.SelectMany(child => child.Dependencies());

		public IEnumerator<IAssetFile<TAsset>> GetEnumerator() => Children.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		void IDisposable.Dispose()
		{
			Unload();
			_children.Clear();
		}
	}
}