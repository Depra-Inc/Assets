// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

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
	}
}