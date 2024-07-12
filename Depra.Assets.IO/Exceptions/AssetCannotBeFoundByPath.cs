// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Assets.IO.Exceptions
{
	internal sealed class AssetCannotBeFoundByPath : Exception
	{
		public AssetCannotBeFoundByPath(string assetName, string path) : base(
			$"Asset '{assetName}' not found by path '{path}'!") { }
	}
}