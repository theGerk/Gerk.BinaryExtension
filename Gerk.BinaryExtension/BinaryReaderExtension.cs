using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gerk.BinaryExtension
{
	public static class BinaryReaderExtension
	{
		internal const uint GUID_SIZE = 16;
		public static Guid ReadGuid(this BinaryReader br) => new Guid(br.ReadBytes((int)GUID_SIZE));
		public static DateTime ReadDateTime(this BinaryReader br) => DateTime.FromBinary(br.ReadInt64());
		public static TimeSpan ReadTimeSpan(this BinaryReader br) => TimeSpan.FromTicks(br.ReadInt64());
		public static byte[] ReadBinaryData(this BinaryReader br) => br.ReadBytes((int)br.ReadUInt32());
		/// <summary>
		/// Reads does the same as <see cref="ReadBinaryData(BinaryReader)"/>, but limits the size.
		/// If the data would be larger than <paramref name="maxSize"/> or is negative it will not read it at all and the reader would have only advanced 4 bytes reading the size.
		/// </summary>
		/// <param name="br"></param>
		/// <param name="maxSize">The maximum expected size of the data in bytes. Expected to be positive.</param>
		/// <param name="data">The resulting data.</param>
		/// <returns>If the data was able to be read (smaller than <paramref name="maxSize"/> and positive).</returns>
		public static bool TryReadBinaryData(this BinaryReader br, out byte[] data, int maxSize = int.MaxValue)
		{
			var size = br.ReadUInt32();
			if (size > (uint)maxSize)
			{
				data = default;
				return false;
			}
			else
			{
				data = br.ReadBytes((int)size);
				return true;
			}
		}

		// Support nulls
		public static bool? ReadNullableBoolean(this BinaryReader br)
		{
			byte read = br.ReadByte();
			if ((read & 1) == 0)
				return null;
			else
				return Convert.ToBoolean(read >> 1);
		}
		public static Byte? ReadNullableByte(this BinaryReader br)
		{
			if (br.ReadBoolean())
				return br.ReadByte();
			else
				return null;
		}
		public static UInt16? ReadNullableUInt16(this BinaryReader br)
		{
			if (br.ReadBoolean())
				return br.ReadUInt16();
			else
				return null;
		}
		public static UInt32? ReadNullableUInt32(this BinaryReader br)
		{
			if (br.ReadBoolean())
				return br.ReadUInt32();
			else
				return null;
		}
		public static UInt64? ReadNullableUInt64(this BinaryReader br)
		{
			if (br.ReadBoolean())
				return br.ReadUInt64();
			else
				return null;
		}
		public static SByte? ReadNullableSByte(this BinaryReader br)
		{
			if (br.ReadBoolean())
				return br.ReadSByte();
			else
				return null;
		}
		public static Int16? ReadNullableInt16(this BinaryReader br)
		{
			if (br.ReadBoolean())
				return br.ReadInt16();
			else
				return null;
		}
		public static Int32? ReadNullableInt32(this BinaryReader br)
		{
			if (br.ReadBoolean())
				return br.ReadInt32();
			else
				return null;
		}
		public static Int64? ReadNullableInt64(this BinaryReader br)
		{
			if (br.ReadBoolean())
				return br.ReadInt64();
			else
				return null;
		}
		public static Decimal? ReadNullableDecimal(this BinaryReader br)
		{
			if (br.ReadBoolean())
				return br.ReadDecimal();
			else
				return null;
		}
		public static Single? ReadNullableSingle(this BinaryReader br)
		{
			if (br.ReadBoolean())
				return br.ReadSingle();
			else
				return null;
		}
		public static Double? ReadNullableDouble(this BinaryReader br)
		{
			if (br.ReadBoolean())
				return br.ReadDouble();
			else
				return null;
		}
		public static Guid? ReadNullableGuid(this BinaryReader br)
		{
			if (br.ReadBoolean())
				return br.ReadGuid();
			else
				return null;
		}
		public static string ReadNullableString(this BinaryReader br)
		{
			if (br.ReadBoolean())
				return br.ReadString();
			else
				return null;
		}
		public static byte[] ReadNullableBinaryData(this BinaryReader br)
		{
			if (br.ReadBoolean())
				return br.ReadBinaryData();
			else
				return null;
		}
		/// <summary>
		/// Reads does the same as <see cref="ReadBinaryData(BinaryReader)"/>, but limits the size.
		/// If the data would be larger than <paramref name="maxSize"/> or is negative it will not read it at all and the reader would have only advanced 4 bytes reading the size.
		/// </summary>
		/// <param name="br"></param>
		/// <param name="maxSize">The maximum expected size of the data in bytes. Expected to be positive.</param>
		/// <param name="data">The resulting data.</param>
		/// <returns>If the data was able to be read (smaller than <paramref name="maxSize"/> and positive).</returns>
		public static bool TryReadNullableBinaryData(this BinaryReader br, out byte[] data, int maxSize = int.MaxValue)
		{
			if (br.ReadBoolean())
				return br.TryReadBinaryData(out data, maxSize);
			else
			{
				data = null;
				return true;
			}
		}
		public static DateTime? ReadNullableDateTime(this BinaryReader br)
		{
			if (br.ReadBoolean())
				return br.ReadDateTime();
			else
				return null;
		}
		public static TimeSpan? ReadNullableTimeSpan(this BinaryReader br)
		{
			if (br.ReadBoolean())
				return br.ReadTimeSpan();
			else
				return null;
		}
	}
}
