﻿// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Assets.Files;
using Depra.Assets.ValueObjects;

namespace Depra.Assets.QoL
{
	public interface IAssetStorage
	{
		void Add(IAssetFile file);

		IAssetFile Get(IAssetUri uri);

		IAssetFile<TAsset> Get<TAsset>(IAssetUri uri);
	}
}