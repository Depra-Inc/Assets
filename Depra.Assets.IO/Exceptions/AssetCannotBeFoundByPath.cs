using System;

namespace Depra.Assets.IO.Exceptions
{
	internal sealed class AssetCannotBeFoundByPath : Exception
	{
		public AssetCannotBeFoundByPath(string assetName, string path) : base(
			$"Asset '{assetName}' not found by path '{path}'!") { }
	}
}