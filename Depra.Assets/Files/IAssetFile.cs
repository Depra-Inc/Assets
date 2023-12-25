// SPDX-License-Identifier: Apache-2.0
// © 2023 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using Depra.Assets.ValueObjects;

namespace Depra.Assets.Files
{
	public interface IAssetFile
	{
		/// <summary>
		/// Returns a value indicating whether the asset is loaded.
		/// </summary>
		bool IsLoaded { get; }

		/// <summary>
		/// Returns the metadata of the asset.
		/// </summary>
		AssetMetadata Metadata { get; }

		/// <summary>
		/// Unloads the asset.
		/// </summary>
		void Unload();

		/// <summary>
		/// Return asset dependencies.
		/// </summary>
		IEnumerable<IAssetUri> Dependencies();
	}
}