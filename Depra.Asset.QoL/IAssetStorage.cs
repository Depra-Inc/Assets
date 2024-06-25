// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Asset.Files;
using Depra.Asset.ValueObjects;

namespace Depra.Asset.QoL
{
	public interface IAssetStorage
	{
		void Add(IAssetFile file);

		IAssetFile Get(IAssetUri uri);

		IAssetFile<TAsset> Get<TAsset>(IAssetUri uri);
	}
}