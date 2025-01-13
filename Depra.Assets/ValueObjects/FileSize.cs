// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Assets
{
	[Serializable]
	public readonly struct FileSize : IEquatable<FileSize>
	{
		public static FileSize Zero => new(0);
		public static FileSize Unknown => new(-1);

		public readonly long Bytes;
		public readonly double Kilobytes;
		public readonly double Megabytes;

		public FileSize(long bytes)
		{
			Bytes = bytes;
			Kilobytes = (double) Bytes / 1024;
			Megabytes = (double) Bytes / (1024 * 1024);
		}

		public bool Equals(FileSize other) =>
			Megabytes.Equals(other.Megabytes) &&
			Kilobytes.Equals(other.Kilobytes) &&
			Bytes == other.Bytes;

		public override bool Equals(object other) =>
			other is FileSize size && Equals(size);

		public override int GetHashCode() =>
			HashCode.Combine(Bytes, Kilobytes, Megabytes);

		public override string ToString() => this.ToHumanReadableString();
	}

	public static class FileSizeExtensions
	{
		private const string SIZE_IN_BYTES_FORMAT = "{0} B";
		private const string SIZE_IN_KILOBYTES_FORMAT = "{0} KB";
		private const string SIZE_IN_MEGABYTES_FORMAT = "{0} MB";

		public static string ToHumanReadableString(this FileSize fileSize) => fileSize switch
		{
			{ Megabytes: > 1 } => string.Format(SIZE_IN_MEGABYTES_FORMAT, fileSize.Megabytes),
			{ Kilobytes: > 1 } => string.Format(SIZE_IN_KILOBYTES_FORMAT, fileSize.Kilobytes),
			_ => string.Format(SIZE_IN_BYTES_FORMAT, fileSize.Bytes)
		};
	}
}