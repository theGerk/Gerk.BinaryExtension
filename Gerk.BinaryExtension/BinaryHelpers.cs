﻿using System;
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
    }
}
