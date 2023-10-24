// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Depra.Assets.Exceptions
{
	internal sealed class AssetAlreadyAddedToGroup : Exception
	{
		public AssetAlreadyAddedToGroup(string assetName) : base($"Asset '{assetName}' already added to group!") { }
	}
}