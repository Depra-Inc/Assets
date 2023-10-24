using System;

namespace Depra.Assets.IO.Exceptions
{
	internal sealed class AssetNotLoaded : Exception
	{
		public AssetNotLoaded(string assetName) : base($"Asset '{assetName}' is not loaded!") { }
	}
}