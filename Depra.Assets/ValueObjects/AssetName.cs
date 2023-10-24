// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Depra.Assets.ValueObjects
{
	public sealed record AssetName(string Name) : IAssetUri
	{
		public static AssetName Empty => new(string.Empty);
		public static AssetName Invalid => new(nameof(Invalid));

		public static implicit operator string(AssetName assetName) => assetName.Name;

		public string Name { get; } = Name;

		string IAssetUri.Absolute => Name;
		string IAssetUri.Relative => Name;
	}
}