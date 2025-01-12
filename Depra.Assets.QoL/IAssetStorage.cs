// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Assets.QoL
{
	public interface IAssetStorage
	{
		void Add(IAssetFile file);

		IAssetFile Get(IAssetUri uri);
		IAssetFile<TAsset> Get<TAsset>(IAssetUri uri);
	}
}