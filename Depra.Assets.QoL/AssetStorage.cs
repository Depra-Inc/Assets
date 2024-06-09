// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Linq;
using Depra.Assets.Files;
using Depra.Assets.ValueObjects;

namespace Depra.Assets.QoL
{
	public sealed class AssetStorage : IAssetStorage
	{
		private readonly List<IAssetFile> _files;

		public AssetStorage() => _files = new List<IAssetFile>();

		public IAssetFile<TAsset> Get<TAsset>(IAssetUri uri) => (IAssetFile<TAsset>) Get(uri);

		public IAssetFile Get(IAssetUri uri) => _files.FirstOrDefault(x => x.Metadata.Uri == uri);

		public void Add(IAssetFile file)
		{
			if (_files.Contains(file) == false)
			{
				_files.Add(file);
			}
		}
	}
}