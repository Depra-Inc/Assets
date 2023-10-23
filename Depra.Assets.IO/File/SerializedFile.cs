// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Depra.Assets.Delegates;
using Depra.Assets.Files;
using Depra.Assets.Idents;
using Depra.Assets.IO.Exceptions;
using Depra.Assets.IO.Ident;
using Depra.Assets.ValueObjects;
using Depra.Serialization.Domain.Interfaces;

namespace Depra.Assets.IO.File
{
	public sealed class SerializedFile<TData> : ILoadableAsset<TData>
	{
		private readonly ISerializer _serializer;
		private readonly FileSystemAssetIdent _ident;

		private TData _deserializedData;

		public SerializedFile(FileSystemAssetIdent ident, ISerializer serializer)
		{
			Guard.AgainstNull(ident, () => new ArgumentNullException(nameof(ident)));
			Guard.AgainstNull(serializer, () => new ArgumentNullException(nameof(serializer)));

			_ident = ident;
			_serializer = serializer;
		}

		public string Name => _ident.Name;
		public string Path => _ident.AbsolutePath;
		public string Extension => _ident.Extension;

		public FileSize Size { get; private set; }
		public bool IsLoaded => _deserializedData != null;

		IAssetIdent IAssetFile.Ident => _ident;

		public TData Load()
		{
			if (_deserializedData != null)
			{
				return _deserializedData;
			}

			Guard.AgainstFileNotExists(_ident.SystemInfo, () => new AssetNotFoundByPathException(Name, Path));

			using var readingStream = _ident.OpenRead();
			_deserializedData = _serializer.Deserialize<TData>(readingStream);

			Guard.AgainstNull(_deserializedData, () => new AssetNotLoadedException(Name));

			Size = FindSize();
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

			Guard.AgainstFileNotExists(_ident.SystemInfo, () => new AssetNotFoundByPathException(Name, Path));
			onProgress?.Invoke(DownloadProgress.Zero);

			await using var readingStream = _ident.OpenRead();
			_deserializedData = await _serializer.DeserializeAsync<TData>(readingStream, cancellationToken);

			onProgress?.Invoke(DownloadProgress.Full);
			Guard.AgainstNull(_deserializedData, () => new AssetNotLoadedException(Name));

			Size = FindSize();
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
		private FileSize FindSize() => new(_ident.SystemInfo.Length);
	}
}