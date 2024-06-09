// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Depra.Assets.Exceptions
{
	internal static class Guard
	{
		private const string TRUE = "DEBUG";
		private const string FALSE = "THIS_IS_JUST_SOME_RANDOM_STRING_THAT_IS_NEVER_DEFINED";

#if DEBUG || DEV_BUILD
		private const string ENSURE = TRUE;
#else
		private const string ENSURE = FALSE;
#endif

		[Conditional(ENSURE)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AgainstNull<TObject>(TObject asset, Func<Exception> exception) =>
			Against(asset == null, exception);

		[Conditional(ENSURE)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AgainstAlreadyContains<T>(T element, IList<T> @in, Func<Exception> exception) =>
			Against(@in.Contains(element), exception);

		[Conditional(ENSURE)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Against(bool condition, Func<Exception> exception)
		{
			if (condition)
			{
				throw exception();
			}
		}
	}
}