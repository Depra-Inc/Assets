// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Assets
{
	public interface IAssetUri : IEquatable<IAssetUri>
	{
		string Absolute { get; }
		string Relative { get; }

		bool IEquatable<IAssetUri>.Equals(IAssetUri other) =>
			other is not null && Absolute == other.Absolute;
	}
}