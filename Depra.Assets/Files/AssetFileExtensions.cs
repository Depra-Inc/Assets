// SPDX-License-Identifier: Apache-2.0
// © 2023 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Linq;
using Depra.Assets.ValueObjects;

namespace Depra.Assets.Files
{
	/// <summary>
	/// Provides extension methods for working with collections of loadable asset files.
	/// </summary>
	public static class AssetFileExtensions
	{
		/// <summary>
		/// Calculates the total size of all <see cref="IAssetFile"/>'s in the collection.
		/// </summary>
		/// <typeparam name="TAsset">The type of the assets file in the collection.</typeparam>
		/// <param name="self">The collection of loadable assets.</param>
		/// <returns>The total size of all loadable assets in the collection as a <see cref="FileSize"/> object.</returns>
		/// <remarks>
		/// The method iterates over each loadable asset in
		/// the collection and retrieves the size of each asset
		/// using the <see cref="AssetMetadata.Size"/> property.
		/// The sizes are summed up to calculate the total size of all assets in bytes.
		/// The total size is returned as a <see cref="FileSize"/> object
		/// that provides convenient methods for converting and formatting the size.
		/// </remarks>
		public static FileSize SizeForAll<TAsset>(this IEnumerable<TAsset> self) where TAsset : IAssetFile =>
			new(self.Sum(x => x.Metadata.Size.Bytes));
	}
}