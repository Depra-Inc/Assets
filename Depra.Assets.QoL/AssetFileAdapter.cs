// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Depra.Assets.Delegates;
using Depra.Assets.Files;
using Depra.Assets.ValueObjects;

namespace Depra.Assets.QoL
{
	public sealed class AssetFileAdapter<TFrom, TTo> : IAssetFile<TTo> where TFrom : class, TTo
	{
		private readonly IAssetFile<TFrom> _inner;

		public AssetFileAdapter(IAssetFile<TFrom> inner) => _inner = inner;

		bool IAssetFile.IsLoaded => _inner.IsLoaded;
		AssetMetadata IAssetFile.Metadata => _inner.Metadata;

		TTo IAssetFile<TTo>.Load() => _inner.Load();
		void IAssetFile.Unload() => _inner.Unload();

		async Task<TTo> IAssetFile<TTo>.LoadAsync(DownloadProgressDelegate onProgress,
			CancellationToken cancellationToken) => await _inner.LoadAsync(onProgress, cancellationToken);

		IEnumerable<IAssetUri> IAssetFile.Dependencies() => _inner.Dependencies();
	}
}