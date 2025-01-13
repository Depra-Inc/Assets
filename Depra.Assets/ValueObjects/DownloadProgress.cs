// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Globalization;

namespace Depra.Assets
{
	/// <summary>
	/// Represents the progress of a download operation.
	/// </summary>
	[Serializable]
	public readonly struct DownloadProgress : IEquatable<DownloadProgress>
	{
		public static DownloadProgress Full => new(1f);
		public static DownloadProgress Zero => new(0f);

		public readonly float NormalizedValue;

		public DownloadProgress(float normalizedValue) =>
			NormalizedValue = normalizedValue;

		public bool Equals(DownloadProgress other) =>
			NormalizedValue.Equals(other.NormalizedValue);

		public override bool Equals(object other) =>
			other is DownloadProgress progress && Equals(progress);

		public override int GetHashCode() =>
			NormalizedValue.GetHashCode();

		public override string ToString() =>
			NormalizedValue.ToString(CultureInfo.InvariantCulture);
	}
}