using System;

namespace Depra.Assets.IO.Exceptions
{
	internal sealed class AssetNotFoundByPathException : Exception
	{
		private const string MESSAGE_FORMAT = "Asset {0} not found by path {1}!";

		public AssetNotFoundByPathException(string assetName, string path)
			: base(string.Format(MESSAGE_FORMAT, assetName, path)) { }
	}
}