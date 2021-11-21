using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gerk.BinaryExtension.Tests
{
	public static class BinaryHelperTests
	{
		[Fact]
		public static void Test_Short()
		{
			byte[] a = new byte[] { 1, 5 }, b = new byte[] { 2, 3 }, c = new byte[] { 3, 6 };
			a.xor(b);
			Assert.True(a.SequenceEquals(c));
		}

		[Fact]
		public static void Test_Long()
		{
			const int length = 100;
			byte[] a = new byte[length], b = new byte[length];
			Random rand = new Random();
			rand.NextBytes(a);
			rand.NextBytes(b);
			byte[] c = new byte[length];
			for (int i = 0; i < length; i++)
			{
				c[i] = a[i];
				c[i] ^= b[i];
			}
			a.xor(b);
			Assert.True(a.SequenceEquals(c));
		}
	}
}
