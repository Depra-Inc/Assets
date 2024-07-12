// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Assets.ValueObjects;

namespace Depra.Assets.Delegates
{
	/// <summary>
	/// Represents a delegate that handles download progress events.
	/// </summary>
	/// <param name="progress">The download progress information.</param>
	public delegate void DownloadProgressDelegate(DownloadProgress progress);
}