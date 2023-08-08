// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using Depra.Assets.Idents;
using Depra.Assets.IO.Extensions;

namespace Depra.Assets.IO.Ident
{
	public sealed class FileSystemAssetIdent : IAssetIdent
	{
		internal readonly FileInfo SystemInfo;

		public static FileSystemAssetIdent Empty => new(string.Empty);
		public static FileSystemAssetIdent Invalid => new(nameof(Invalid));

		public FileSystemAssetIdent(FileInfo systemInfo, string relativePath = "")
		{
			SystemInfo = systemInfo;
			RelativePath = relativePath;
			SystemInfo.Directory.CreateIfNotExists();
			Name = SystemInfo.Name.Replace(Extension, string.Empty);
		}

		public FileSystemAssetIdent(string path, string relativePath = "")
			: this(new FileInfo(path), relativePath) { }

		public FileSystemAssetIdent(string nameWithExtension, string directory, string relativePath = "")
			: this(Path.Combine(directory, nameWithExtension), relativePath) { }

		public FileSystemAssetIdent(string name, string directory, string extension = null, string relativePath = null)
			: this(Path.Combine(directory, name + extension), relativePath) { }

		public string Name { get; }

		public string Extension => SystemInfo.Extension;

		public string AbsolutePath => SystemInfo.FullName;

		public string NameWithExtension => Name + Extension;

		public string RelativePath { get; }

		public string AbsoluteDirectoryPath => SystemInfo.DirectoryName;

		public string RelativeDirectoryPath => SystemInfo.Directory!.Name;

		string IAssetIdent.Uri => AbsolutePath;

		string IAssetIdent.RelativeUri => RelativePath;
	}

	public static class FileSystemAssetIdentExtensions
	{
		public static Stream OpenRead(this FileSystemAssetIdent self) =>
			self.SystemInfo.OpenRead();

		public static Stream OpenWrite(this FileSystemAssetIdent self) =>
			self.SystemInfo.OpenWrite();
	}
}