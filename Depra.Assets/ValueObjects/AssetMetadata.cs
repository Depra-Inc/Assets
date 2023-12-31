﻿// SPDX-License-Identifier: Apache-2.0
// © 2023 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Assets.ValueObjects
{
	public sealed record AssetMetadata(IAssetUri Uri, FileSize Size)
	{
		/// <summary>
		/// Returns the Uri of the asset.
		/// </summary>
		public IAssetUri Uri { get; } = Uri;

		/// <summary>
		/// Returns the size of the asset.
		/// </summary>
		public FileSize Size { get; set; } = Size;
	}
}