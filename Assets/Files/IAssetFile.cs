// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.Assets.Idents;
using Depra.Assets.ValueObjects;

namespace Depra.Assets.Files
{
    public interface IAssetFile
    {
        /// <summary>
        /// Returns the reference ID of the asset.
        /// </summary>
        IAssetIdent Ident { get; }

        /// <summary>
        /// Returns the size of the asset.
        /// </summary>
        FileSize Size { get; }
    }
}