// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using JetBrains.Annotations;

namespace Depra.Assets.Idents
{
    public interface IAssetIdent
    {
        public string Uri { get; }

        [UsedImplicitly]
        public string RelativeUri { get; }
    }
}