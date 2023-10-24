// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Depra.Assets.ValueObjects
{
	public interface IAssetUri
	{
		public string Absolute { get; }

		public string Relative { get; }
	}
}