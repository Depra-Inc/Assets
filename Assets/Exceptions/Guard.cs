// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Depra.Assets.Exceptions
{
    internal static class Guard
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AgainstNull<TObject>(TObject asset, Func<Exception> exceptionFunc)
        {
            if (asset == null)
            {
                throw exceptionFunc();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AgainstAlreadyContains<T>(T element, List<T> @in, Func<Exception> exceptionFunc)
        {
            if (@in.Contains(element))
            {
                throw exceptionFunc();
            }
        }
    }
}