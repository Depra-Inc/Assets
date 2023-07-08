// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Globalization;

namespace Depra.Assets.ValueObjects
{
    /// <summary>
    /// Represents the progress of a download operation.
    /// </summary>
    [Serializable]
    public readonly struct DownloadProgress : IEquatable<DownloadProgress>
    {
        private const float MIN_VALUE = 0f;
        private const float MAX_VALUE = 1f;

        public static DownloadProgress Full => new DownloadProgress(MAX_VALUE);
        public static DownloadProgress Zero => new DownloadProgress(MIN_VALUE);

        public readonly float NormalizedValue;

        public DownloadProgress(float normalizedValue) =>
            NormalizedValue = normalizedValue;

        public bool Equals(DownloadProgress other) =>
            NormalizedValue.Equals(other.NormalizedValue);

        public override bool Equals(object obj) =>
            obj is DownloadProgress other && Equals(other);

        public override int GetHashCode() =>
            NormalizedValue.GetHashCode();

        public override string ToString() =>
            NormalizedValue.ToString(CultureInfo.InvariantCulture);
    }
}