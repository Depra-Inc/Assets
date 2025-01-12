// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Threading;
using Depra.Threading;

namespace Depra.Assets.QoL
{
	public sealed class AssetFileAdapter<TFrom, TTo> : IAssetFile<TTo> where TFrom : class, TTo
	{
		private readonly IAssetFile<TFrom> _inner;

		public AssetFileAdapter(IAssetFile<TFrom> inner) => _inner = inner;

		bool IAssetFile.IsLoaded => _inner.IsLoaded;
		AssetMetadata IAssetFile.Metadata => _inner.Metadata;

		void IAssetFile.Unload() => _inner.Unload();

		TTo IAssetFile<TTo>.Load() => _inner.Load();

		async ITask<TTo> IAssetFile<TTo>.LoadAsync(DownloadProgressDelegate onProgress, CancellationToken cancellation) =>
			await _inner.LoadAsync(onProgress, cancellation);

		IEnumerable<IAssetUri> IAssetFile.Dependencies() => _inner.Dependencies();
	}
}