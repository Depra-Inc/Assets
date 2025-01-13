// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System.Threading;
using Depra.Threading;

namespace Depra.Assets
{
	/// <summary>
	/// Represents an interface for a loadable asset.
	/// </summary>
	/// <typeparam name="TAsset">The type of the asset to be loaded.</typeparam>
	public interface IAssetFile<out TAsset> : IAssetFile
	{
		/// <summary>
		/// Loads the asset synchronously.
		/// </summary>
		/// <returns>The loaded asset of type <typeparamref name="TAsset"/>.</returns>
		TAsset Load();

		/// <summary>
		/// Loads the asset asynchronously.
		/// </summary>
		/// <param name="onProgress">An optional delegate for tracking the download progress.</param>
		/// <param name="cancellation">A cancellation token that can be used to cancel the async operation.</param>
		/// <returns>A task representing the asynchronous loading operation, returning the loaded asset of type <typeparamref name="TAsset"/>.</returns>
		ITask<TAsset> LoadAsync(DownloadProgressDelegate onProgress = null, CancellationToken cancellation = default);
	}
}