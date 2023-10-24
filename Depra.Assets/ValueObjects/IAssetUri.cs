// SPDX-License-Identifier: Apache-2.0
// © 2023 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Assets.ValueObjects
{
	public interface IAssetUri
	{
		public string Absolute { get; }

		public string Relative { get; }
	}
}