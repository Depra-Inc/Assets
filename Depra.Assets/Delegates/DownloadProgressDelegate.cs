// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Assets
{
	/// <summary>
	/// Represents a delegate that handles download progress events.
	/// </summary>
	/// <param name="progress">The download progress information.</param>
	public delegate void DownloadProgressDelegate(DownloadProgress progress);
}