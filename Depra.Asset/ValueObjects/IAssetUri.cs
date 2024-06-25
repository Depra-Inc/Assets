// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Asset.ValueObjects
{
	public interface IAssetUri
	{
		public string Absolute { get; }

		public string Relative { get; }
	}
}