using System;
using System.IO;

namespace Depra.Assets.IO.Exceptions
{
	internal static class Guard
	{
		public static void AgainstFileNotExists(FileInfo info, Func<Exception> exceptionFunc)
		{
			if (info.Exists == false)
			{
				throw exceptionFunc();
			}
		}
	}
}