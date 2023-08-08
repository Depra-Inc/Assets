// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Depra.Assets.Exceptions
{
	internal sealed class AssetAlreadyLoadedException : Exception
	{
		private const string MESSAGE_FORMAT = "Asset {0} already loaded!";

		public AssetAlreadyLoadedException(string assetName) : base(string.Format(MESSAGE_FORMAT, assetName)) { }
	}
}