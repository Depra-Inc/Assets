// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Asset.ValueObjects
{
	public sealed record AssetName(string Name) : IAssetUri
	{
		public static AssetName Empty => new(string.Empty);
		public static AssetName Invalid => new(nameof(Invalid));

		public static implicit operator AssetName(string name) => new(name);
		public static implicit operator string(AssetName assetName) => assetName.Name;

		public string Name { get; } = Name;

		string IAssetUri.Absolute => Name;
		string IAssetUri.Relative => Name;
	}
}