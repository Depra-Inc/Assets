﻿// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Depra.Assets.Idents
{
	public sealed class AssetName : IAssetIdent
	{
		public static AssetName Empty => new(string.Empty);
		public static AssetName Invalid => new(nameof(Invalid));

		public static implicit operator string(AssetName assetName) => assetName.Name;

		public AssetName(string uri) => Name = uri;

		public string Name { get; }

		string IAssetIdent.Uri => Name;
		string IAssetIdent.RelativeUri => Name;
	}
}