﻿// SPDX-License-Identifier: Apache-2.0
// © 2023 Nikolay Melnikov <n.melnikov@depra.org>

using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Depra.Assets.IO.Extensions
{
	public static class DirectoryInfoExtensions
	{
		public static bool IsEmpty(this DirectoryInfo directoryInfo) =>
			directoryInfo.EnumerateFileSystemInfos().Any() == false;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DirectoryInfo CreateIfNotExists(this DirectoryInfo directoryInfo)
		{
			if (directoryInfo.Exists == false)
			{
				directoryInfo.Create();
			}

			return directoryInfo;
		}
	}
}