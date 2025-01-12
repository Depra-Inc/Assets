// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Linq;
using Depra.Assets.Files;
using Depra.Assets.ValueObjects;

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

		public IAssetFile<TAsset> Get<TAsset>(IAssetUri uri)
		{
			var untyped = Get(uri);
			if (untyped.GetType().IsGenericType) { }

			return Get(uri) as IAssetFile<TAsset>;
		}

		public IAssetFile Get(IAssetUri uri) => _files.FirstOrDefault(x => x.Metadata.Uri == uri);
	}
}