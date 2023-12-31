﻿// SPDX-License-Identifier: Apache-2.0
// © 2023 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using Depra.Assets.Files;

namespace Depra.Assets.Exceptions
{
	internal sealed class AssetCannotBeLoadedFromGroup : Exception
	{
		public AssetCannotBeLoadedFromGroup(IAssetFile asset, string groupName) :
			this(asset.Metadata.Uri.Relative, groupName) { }

		public AssetCannotBeLoadedFromGroup(string assetName, string groupName) :
			base($"Failed to load asset '{assetName}' from a group '{groupName}'!") { }
	}
}