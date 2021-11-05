using System;
using System.Runtime;
using System.Text;
using System.Globalization;
using System.Diagnostics.Contracts;
using System.Security;
using System.IO;
using System.Threading.Tasks;

namespace Gerk.BinaryExtension
{
	public class AsyncBinaryReader : IDisposable
	{
		public Stream BaseStream { get; private set; }
		private bool leaveOpen;

		public AsyncBinaryReader(Stream stream, bool leaveOpen = false)
		{
			BaseStream = stream;
			this.leaveOpen = leaveOpen;
		}

		public void Dispose()
		{
			if (!leaveOpen)
				((IDisposable)BaseStream).Dispose();
		}

		public async Task<int> Read()
	}
}
