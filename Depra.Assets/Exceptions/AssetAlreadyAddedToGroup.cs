// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Assets
{
	internal sealed class AssetAlreadyAddedToGroup : Exception
	{
		public AssetAlreadyAddedToGroup(string assetName) : base($"Asset '{assetName}' already added to group!") { }
	}
}