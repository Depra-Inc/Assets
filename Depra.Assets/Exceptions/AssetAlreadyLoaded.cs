// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Depra.Assets.Exceptions
{
	internal sealed class AssetAlreadyLoaded : Exception
	{
		public AssetAlreadyLoaded(string assetName) : base($"Asset '{assetName}' already loaded!") { }
	}
}