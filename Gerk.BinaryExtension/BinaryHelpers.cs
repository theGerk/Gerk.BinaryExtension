using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gerk.BinaryExtension
{
	public static class BinaryHelpers
	{
		[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int memcmp(byte[] b1, byte[] b2, UIntPtr count);

		public static bool SequenceEquals(this byte[] b1, byte[] b2)
		{
			if (b1 == b2) return true; //reference equality check

			if (b1 == null || b2 == null || b1.Length != b2.Length) return false;

			return memcmp(b1, b2, new UIntPtr((uint)b1.Length)) == 0;
		}

		public static unsafe void xor(this byte[] a, byte[] b)
		{
			if (a.Length != b.Length)
				throw new Exception("xoring arrays of different length");

			fixed (byte* p1 = a)
			fixed (byte* p2 = b)
			{
				var l1 = (long*)p1;
				var l2 = (long*)p2;

				const int scalingFactor = sizeof(long) >> 1;

				int size = a.Length >> scalingFactor;
				long* lend = l1 + size;

				for (; l1 != lend; l1++, l2++)
					*l1 ^= *l2;

				byte* bend = p1 + a.Length;
				for (byte* b1 = (byte*)l1, b2 = (byte*)l2; b1 != bend; b1++, b2++)
					*b1 ^= *b2;
			}
		}
	}
}
