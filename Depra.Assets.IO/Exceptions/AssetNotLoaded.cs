// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Assets.IO.Exceptions
{
	internal sealed class AssetNotLoaded : Exception
	{
		public AssetNotLoaded(string assetName) : base($"Asset '{assetName}' is not loaded!") { }
	}
}