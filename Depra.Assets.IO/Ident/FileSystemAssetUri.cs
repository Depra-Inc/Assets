// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using Depra.Assets.IO.Extensions;
using Depra.Assets.ValueObjects;

namespace Depra.Assets.IO.Ident
{
	public sealed class FileSystemAssetUri : IAssetUri
	{
		public static FileSystemAssetUri Empty => new(string.Empty);
		public static FileSystemAssetUri Invalid => new(nameof(Invalid));

		internal readonly FileInfo SystemInfo;

		public FileSystemAssetUri(FileInfo systemInfo, string relativePath = "")
		{
			SystemInfo = systemInfo;
			RelativePath = relativePath;
			SystemInfo.Directory.CreateIfNotExists();
			Name = SystemInfo.Name.Replace(Extension, string.Empty);
		}

		public FileSystemAssetUri(string path, string relativePath = "")
			: this(new FileInfo(path), relativePath) { }

		public FileSystemAssetUri(string nameWithExtension, string directory, string relativePath = "")
			: this(Path.Combine(directory, nameWithExtension), relativePath) { }

		public FileSystemAssetUri(string name, string directory, string extension = null, string relativePath = null)
			: this(Path.Combine(directory, name + extension), relativePath) { }

		public string Name { get; }

		public string Extension => SystemInfo.Extension;

		public string AbsolutePath => SystemInfo.FullName;

		public string NameWithExtension => Name + Extension;

		public string RelativePath { get; }

		public string AbsoluteDirectoryPath => SystemInfo.DirectoryName;

		public string RelativeDirectoryPath => SystemInfo.Directory!.Name;

		string IAssetUri.Absolute => AbsolutePath;

		string IAssetUri.Relative => RelativePath;
	}
}