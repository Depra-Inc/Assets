// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Linq;

namespace Depra.Assets.QoL
{
	public sealed class AssetStorage : IAssetStorage
	{
		private readonly List<IAssetFile> _files = new();

		public void Add(IAssetFile file)
		{
			if (_files.Contains(file) == false)
			{
				_files.Add(file);
			}
		}

		IAssetFile IAssetStorage.Get(IAssetUri uri) =>
			_files.FirstOrDefault(x => x.Metadata.Uri == uri);

		IAssetFile<TAsset> IAssetStorage.Get<TAsset>(IAssetUri uri) =>
			_files.FirstOrDefault(x => x.Metadata.Uri == uri) as IAssetFile<TAsset>;
	}
}