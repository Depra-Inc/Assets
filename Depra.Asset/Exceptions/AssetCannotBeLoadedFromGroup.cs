// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using Depra.Asset.Files;

namespace Depra.Asset.Exceptions
{
	internal sealed class AssetCannotBeLoadedFromGroup : Exception
	{
		public AssetCannotBeLoadedFromGroup(IAssetFile asset, string groupName) :
			base($"Failed to load asset '{asset.Metadata.Uri.Relative}' from a group '{groupName}'!") { }
	}
}