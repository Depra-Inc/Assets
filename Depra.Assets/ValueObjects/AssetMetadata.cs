// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Assets.ValueObjects
{
	public sealed record AssetMetadata(IAssetUri Uri, FileSize Size)
	{
		/// <summary>
		/// Returns the <see cref="Uri"/> of the asset.
		/// </summary>
		public IAssetUri Uri { get; } = Uri;

		/// <summary>
		/// Returns the <see cref="Size"/> of the asset.
		/// </summary>
		public FileSize Size { get; set; } = Size;
	}
}