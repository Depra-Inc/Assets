// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using Depra.Assets.Files;

namespace Depra.Assets.Exceptions
{
	internal sealed class AssetCannotBeLoadedFromGroup : Exception
	{
		private const string MESSAGE_FORMAT = "Failed to load asset '{0}' from a group '{1}'!";

		public AssetCannotBeLoadedFromGroup(IAssetFile asset, string groupName) :
			this(asset.Ident.RelativeUri, groupName) { }

		public AssetCannotBeLoadedFromGroup(string assetName, string groupName) :
			base(string.Format(MESSAGE_FORMAT, assetName, groupName)) { }
	}
}