// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Assets
{
	internal sealed class AssetAlreadyLoaded : Exception
	{
		public AssetAlreadyLoaded(string assetName) : base($"Asset '{assetName}' already loaded!") { }
	}
}