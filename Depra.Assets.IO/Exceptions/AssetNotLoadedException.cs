using System;

namespace Depra.Assets.IO.Exceptions
{
    public sealed class AssetNotLoadedException : Exception
    {
        private const string MESSAGE_FORMAT = "Asset {0} is not loaded!";

        public AssetNotLoadedException(string assetName)
            : base(string.Format(MESSAGE_FORMAT, assetName)) { }
    }
}