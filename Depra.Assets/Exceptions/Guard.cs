// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Depra.Assets.Exceptions
{
	internal static class Guard
	{
		[Conditional("DEBUG")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AgainstNull<TObject>(TObject asset, Func<Exception> exception)
		{
			if (asset == null)
			{
				throw exception();
			}
		}

		[Conditional("DEBUG")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AgainstAlreadyContains<T>(T element, IList<T> @in, Func<Exception> exception)
		{
			if (@in.Contains(element))
			{
				throw exception();
			}
		}
	}
}