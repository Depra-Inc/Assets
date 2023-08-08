// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.Assets.ValueObjects;

namespace Depra.Assets.Delegates
{
	/// <summary>
	/// Represents a delegate that handles download progress events.
	/// </summary>
	/// <param name="progress">The download progress information.</param>
	public delegate void DownloadProgressDelegate(DownloadProgress progress);
}