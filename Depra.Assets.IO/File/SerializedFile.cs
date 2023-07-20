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
using Depra.Assets.IO.Rules;
using Depra.Assets.ValueObjects;
using Depra.Serialization.Domain.Interfaces;
using static Depra.Assets.IO.Exceptions.Guard;
using static Depra.Assets.Exceptions.Guard;

namespace Depra.Assets.IO.File
{
    public sealed class SerializedFile<TData> :
        ILoadableAsset<TData>,
        IWriteRule<TData>,
        IReadRule<TData>
    {
        private readonly ISerializer _serializer;
        private readonly FileSystemAssetIdent _ident;

        private TData _deserializedData;

        public SerializedFile(FileSystemAssetIdent ident, ISerializer serializer)
        {
            _ident = ident ?? throw new ArgumentNullException(nameof(ident));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public string Name => _ident.Name;
        public string Path => _ident.AbsolutePath;
        public string Extension => _ident.Extension;

        public FileSize Size { get; private set; }
        public bool IsLoaded => _deserializedData != null;

        IAssetIdent IAssetFile.Ident => _ident;

        public bool CanRead() =>
            _ident.SystemInfo.Exists;

        public bool CanWrite() =>
            _ident.SystemInfo.Exists &&
            _ident.SystemInfo.IsReadOnly == false;

        public TData Read()
        {
            if (_deserializedData != null)
            {
                return _deserializedData;
            }

            AgainstFileNotExists(_ident.SystemInfo, () => new AssetNotFoundByPathException(Name, Path));

            using var readingStream = _ident.OpenRead();
            _deserializedData = _serializer.Deserialize<TData>(readingStream);

            AgainstNull(_deserializedData, () => new AssetNotLoadedException(Name));

            Size = FindSize();
            return _deserializedData;
        }

        public async Task<TData> ReadAsync(DownloadProgressDelegate onProgress = null,
            CancellationToken cancellationToken = default)
        {
            if (IsLoaded)
            {
                onProgress?.Invoke(DownloadProgress.Full);
                return _deserializedData;
            }

            AgainstFileNotExists(_ident.SystemInfo, () => new AssetNotFoundByPathException(Name, Path));
            onProgress?.Invoke(DownloadProgress.Zero);

            await using var readingStream = _ident.OpenRead();
            _deserializedData = await _serializer.DeserializeAsync<TData>(readingStream, cancellationToken);

            onProgress?.Invoke(DownloadProgress.Full);
            AgainstNull(_deserializedData, () => new AssetNotLoadedException(Name));

            Size = FindSize();
            return _deserializedData;
        }

        public void Write(TData data)
        {
            using var writingStream = _ident.OpenWrite();
            _serializer.Serialize(writingStream, data);
        }

        public void Unload()
        {
            if (IsLoaded)
            {
                _deserializedData = default;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private FileSize FindSize() => new FileSize(_ident.SystemInfo.Length);

        TData ILoadableAsset<TData>.Load() => Read();

        Task<TData> ILoadableAsset<TData>.LoadAsync(
            DownloadProgressDelegate onProgress,
            CancellationToken cancellationToken) =>
            ReadAsync(onProgress, cancellationToken);
    }
}