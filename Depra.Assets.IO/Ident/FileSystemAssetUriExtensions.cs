// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System.IO;
using System.Runtime.CompilerServices;

namespace Depra.Assets.IO
{
	public static class FileSystemAssetUriExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Stream OpenRead(this FileSystemAssetUri self) => self.SystemInfo.OpenRead();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Stream OpenWrite(this FileSystemAssetUri self) => self.SystemInfo.OpenWrite();
	}
}