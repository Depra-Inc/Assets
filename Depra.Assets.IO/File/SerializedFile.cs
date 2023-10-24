// SPDX-License-Identifier: Apache-2.0
// © 2023 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Depra.Assets.Delegates;
using Depra.Assets.Files;
using Depra.Assets.IO.Exceptions;
using Depra.Assets.IO.Ident;
using Depra.Assets.ValueObjects;
using Depra.Serialization.Domain.Interfaces;

namespace Depra.Assets.IO.File
{
	public sealed class SerializedFile<TData> : IAssetFile<TData>
	{
		private readonly ISerializer _serializer;
		private readonly FileSystemAssetUri _uri;

		private TData _deserializedData;

		public SerializedFile(FileSystemAssetUri uri, ISerializer serializer)
		{
			Guard.AgainstNull(uri, () => new ArgumentNullException(nameof(uri)));
			Guard.AgainstNull(serializer, () => new ArgumentNullException(nameof(serializer)));

			_serializer = serializer;
			Metadata = new AssetMetadata(_uri = uri, FileSize.Unknown);
		}

		public string Name => _uri.Name;
		public AssetMetadata Metadata { get; }
		public string Path => _uri.AbsolutePath;
		public string Extension => _uri.Extension;
		public bool IsLoaded => _deserializedData != null;

		public TData Load()
		{
			if (_deserializedData != null)
			{
				return _deserializedData;
			}

			Guard.AgainstFileNotExists(_uri.SystemInfo, () => new AssetCannotBeFoundByPath(Name, Path));

			using var readingStream = _uri.OpenRead();
			_deserializedData = _serializer.Deserialize<TData>(readingStream);

			Guard.AgainstNull(_deserializedData, () => new AssetNotLoaded(Name));

			Metadata.Size = FindSize();
			return _deserializedData;
		}

		public async Task<TData> LoadAsync(DownloadProgressDelegate onProgress,
			CancellationToken cancellationToken = default)
		{
			if (IsLoaded)
			{
				onProgress?.Invoke(DownloadProgress.Full);
				return _deserializedData;
			}

			Guard.AgainstFileNotExists(_uri.SystemInfo, () => new AssetCannotBeFoundByPath(Name, Path));
			onProgress?.Invoke(DownloadProgress.Zero);

			await using var readingStream = _uri.OpenRead();
			_deserializedData = await _serializer.DeserializeAsync<TData>(readingStream, cancellationToken);

			onProgress?.Invoke(DownloadProgress.Full);
			Guard.AgainstNull(_deserializedData, () => new AssetNotLoaded(Name));

			Metadata.Size = FindSize();
			return _deserializedData;
		}

		public void Unload()
		{
			if (IsLoaded)
			{
				_deserializedData = default;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private FileSize FindSize() => new(_uri.SystemInfo.Length);
	}
}