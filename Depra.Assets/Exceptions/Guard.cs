﻿// SPDX-License-Identifier: Apache-2.0
// © 2023 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Depra.Assets.Exceptions
{
	internal static class Guard
	{
		private const string CONDITION = "DEBUG";

		[Conditional(CONDITION)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AgainstNull<TObject>(TObject asset, Func<Exception> exception)
		{
			if (asset == null)
			{
				throw exception();
			}
		}

		[Conditional(CONDITION)]
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