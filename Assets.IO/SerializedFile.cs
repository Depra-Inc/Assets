using System;
using System.Threading;
using System.Threading.Tasks;
using Depra.Assets.Delegates;
using Depra.Assets.Files;
using Depra.Assets.Idents;
using Depra.Assets.ValueObjects;
using Depra.Serialization.Domain.Interfaces;

namespace Assets.IO
{
    public sealed class SerializedFile<TData> : ILoadableAsset<object>
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

        public bool IsLoaded => _deserializedData != null;
        public FileSize Size => new(_ident.SystemInfo.Length);
        
        IAssetIdent IAssetFile.Ident => _ident;

        public bool CanRead() => _ident.SystemInfo.Exists;

        public bool CanWrite() => _ident.SystemInfo.Exists && _ident.SystemInfo.IsReadOnly == false;

        public object Read()
        {
            if (_deserializedData != null)
            {
                return _deserializedData;
            }

            using var readingStream = _ident.SystemInfo.OpenRead();
            _deserializedData = _serializer.Deserialize<TData>(readingStream);

            return _deserializedData;
        }

        public async Task<object> ReadAsync(DownloadProgressDelegate onProgress = null,
            CancellationToken cancellationToken = default)
        {
            onProgress?.Invoke(DownloadProgress.Zero);

            await using var readingStream = _ident.SystemInfo.OpenRead();
            _deserializedData = await _serializer.DeserializeAsync<TData>(readingStream, cancellationToken);

            onProgress?.Invoke(DownloadProgress.Full);

            return _deserializedData;
        }

        public void Write(TData data)
        {
            using var writingStream = _ident.SystemInfo.OpenWrite();
            _serializer.Serialize(writingStream, data);
        }

        object ILoadableAsset<object>.Load() => Read();

        Task<object> ILoadableAsset<object>.LoadAsync(
            DownloadProgressDelegate onProgress,
            CancellationToken cancellationToken) =>
            ReadAsync(onProgress, cancellationToken);

        void ILoadableAsset<object>.Unload()
        {
            if (IsLoaded)
            {
                _deserializedData = default;
            }
        }
    }
}