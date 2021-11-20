using System;
using System.IO;
using Xunit;

namespace Gerk.BinaryExtension.Tests
{
	public class BinaryReaderAndWriterExtensionTests
	{
		public static (BinaryReader reader, BinaryWriter writer, MemoryStream mem) init()
		{
			var mem = new MemoryStream();
			return (new(mem), new(mem), mem);
		}

		public static void testBody<T>(Action<BinaryWriter, T> write, Func<BinaryReader, T> read, T value = default, Func<T, T, bool>? equality = null)
		{
			var (reader, writer, mem) = init();
			write(writer, value);
			mem.Position = 0;
			if (equality == null)
				Assert.Equal(read(reader), value);
			else
				Assert.True(equality(read(reader), value));
		}

		[Fact]
		public void guid() => testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadGuid, Guid.NewGuid());

		[Fact]
		public void datetime() => testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadDateTime, DateTime.Now);

		[Fact]
		public void timespan() => testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadTimeSpan, DateTime.Now - new DateTime(2020, 01, 01));

		[Fact]
		public void binarydata() => testBody(BinaryWriterExtensions.WriteBinaryData, BinaryReaderExtension.ReadBinaryData, Guid.NewGuid().ToByteArray(), (a, b) => a.SequenceEquals(b));

		[Fact]
		public void trybinarydat_Negative()
		{
			testBody((writer, _) => writer.Write(-1), reader => reader.TryReadBinaryData(out var _), equality: (a, _) => !a);
		}

		[Fact]
		public void trybinarydat_ToBig()
		{
			testBody((writer, _) => writer.WriteBinaryData(new byte[100]), reader => reader.TryReadBinaryData(out var _, 10), equality: (a, _) => !a);
		}

		[Fact]
		public void trybinarydat_Working()
		{
			testBody(BinaryWriterExtensions.WriteBinaryData, reader => { reader.TryReadBinaryData(out var d, 1000); return d; }, Guid.NewGuid().ToByteArray(), (a, b) => a.SequenceEquals(b));
		}

		[Fact]
		public void nullbool()
		{
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableBoolean, null);
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableBoolean, true);
		}

		[Fact]
		public void nullbyte()
		{
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableByte, null);
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableByte, (byte)10);
		}

		[Fact]
		public void nulluint16()
		{
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableUInt16, null);
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableUInt16, (ushort)10);
		}
		[Fact]
		public void nulluint32()
		{
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableUInt32, null);
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableUInt32, (uint)10);
		}
		[Fact]
		public void nulluint64()
		{
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableUInt64, null);
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableUInt64, (ulong)10);
		}
		[Fact]
		public void nullsbyte()
		{
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableSByte, null);
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableSByte, (sbyte)10);
		}
		[Fact]
		public void nullint16()
		{
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableInt16, null);
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableInt16, (short)10);
		}
		[Fact]
		public void nullint32()
		{
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableInt32, null);
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableInt32, 10);
		}
		[Fact]
		public void nullint64()
		{
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableInt64, null);
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableInt64, 10);
		}
		[Fact]
		public void nulldecimal()
		{
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableDecimal, null);
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableDecimal, 10);
		}
		[Fact]
		public void nullsingle()
		{
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableSingle, null);
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableSingle, 10);
		}
		[Fact]
		public void nulldouble()
		{
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableDouble, null);
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableDouble, 10);
		}
		[Fact]
		public void nullguid()
		{
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableGuid, null);
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableGuid, Guid.NewGuid());
		}
		[Fact]
		public void nullstring()
		{
			testBody(BinaryWriterExtensions.WriteNullable, BinaryReaderExtension.ReadNullableString, null);
			testBody(BinaryWriterExtensions.WriteNullable, BinaryReaderExtension.ReadNullableString, Guid.NewGuid().ToString());
		}
		[Fact]
		public void nullbindat()
		{
			testBody(BinaryWriterExtensions.WriteNullableBinaryData, BinaryReaderExtension.ReadNullableBinaryData, null, (a, b) => a.SequenceEquals(b));
			testBody(BinaryWriterExtensions.WriteNullableBinaryData, BinaryReaderExtension.ReadNullableBinaryData, Guid.NewGuid().ToByteArray(), (a, b) => a.SequenceEquals(b));
		}
		[Fact]
		public void trynullbindat()
		{
			testBody(BinaryWriterExtensions.WriteNullableBinaryData, br => {
				var b = br.TryReadNullableBinaryData(out var a);
				Assert.True(b);
				return a; 
			}, null, (a, b) => a.SequenceEquals(b)); 
			testBody(BinaryWriterExtensions.WriteNullableBinaryData, br => {
				var b = br.TryReadNullableBinaryData(out var a);
				Assert.True(b);
				return a;
			}, Guid.NewGuid().ToByteArray(), (a, b) => a.SequenceEquals(b));
			testBody(BinaryWriterExtensions.WriteNullableBinaryData, br => {
				var b = br.TryReadNullableBinaryData(out var a, 1);
				Assert.False(b);
				return a;
			}, Guid.NewGuid().ToByteArray(), (a, _) => a == null);
		}
		[Fact]
		public void nulldatetime()
		{
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableDateTime, null);
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableDateTime, DateTime.Now);
		}
		[Fact]
		public void nulltimespan()
		{
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableTimeSpan, null);
			testBody(BinaryWriterExtensions.Write, BinaryReaderExtension.ReadNullableTimeSpan, new DateTime(2020, 1, 1) - DateTime.Now);
		}
	}
}