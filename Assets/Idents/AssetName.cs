// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Depra.Assets.Idents
{
    public sealed class AssetName : IAssetIdent
    {
        public static AssetName Empty => new AssetName(string.Empty);
        public static AssetName Invalid => new AssetName(nameof(Invalid));

        public AssetName(string uri) => Name = uri;

        public string Name { get; }

        string IAssetIdent.Uri => Name;
        string IAssetIdent.RelativeUri => Name;
    }
}