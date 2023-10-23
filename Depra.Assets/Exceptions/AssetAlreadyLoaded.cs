﻿// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Depra.Assets.Exceptions
{
	internal sealed class AssetAlreadyLoaded : Exception
	{
		private const string MESSAGE_FORMAT = "Asset {0} already loaded!";

		public AssetAlreadyLoaded(string assetName) : base(string.Format(MESSAGE_FORMAT, assetName)) { }
	}
}