using System;
using System.IO;

namespace Gerk.BinaryExtension
{
	/// <summary>
	/// Holds writer extensions for new types
	/// </summary>
	public static class BinaryWriterExtensions
	{
		public static void Write(this BinaryWriter bw, Guid value) => bw.Write(value.ToByteArray());
		public static void Write(this BinaryWriter bw, DateTime value) => bw.Write(value.ToBinary());
		public static void Write(this BinaryWriter bw, TimeSpan value) => bw.Write(value.Ticks);
		public static void WriteBinaryData(this BinaryWriter bw, byte[] value)
		{
			bw.Write(value.Length);
			bw.Write(value);
		}

		// Support nulls
		public static void Write(this BinaryWriter bw, bool? value)
		{
			byte write = Convert.ToByte(value ?? false);
			write <<= 1;
			write |= Convert.ToByte(value.HasValue);
			bw.Write(write);
		}
		public static void Write(this BinaryWriter bw, byte? value)
		{
			bw.Write(value.HasValue);
			if (value.HasValue)
				bw.Write(value.Value);
		}
		public static void Write(this BinaryWriter bw, ushort? value)
		{
			bw.Write(value.HasValue);
			if (value.HasValue)
				bw.Write(value.Value);
		}
		public static void Write(this BinaryWriter bw, uint? value)
		{
			bw.Write(value.HasValue);
			if (value.HasValue)
				bw.Write(value.Value);
		}
		public static void Write(this BinaryWriter bw, ulong? value)
		{
			bw.Write(value.HasValue);
			if (value.HasValue)
				bw.Write(value.Value);
		}
		public static void Write(this BinaryWriter bw, sbyte? value)
		{
			bw.Write(value.HasValue);
			if (value.HasValue)
				bw.Write(value.Value);
		}
		public static void Write(this BinaryWriter bw, short? value)
		{
			bw.Write(value.HasValue);
			if (value.HasValue)
				bw.Write(value.Value);
		}
		public static void Write(this BinaryWriter bw, int? value)
		{
			bw.Write(value.HasValue);
			if (value.HasValue)
				bw.Write(value.Value);
		}
		public static void Write(this BinaryWriter bw, long? value)
		{
			bw.Write(value.HasValue);
			if (value.HasValue)
				bw.Write(value.Value);
		}
		public static void Write(this BinaryWriter bw, decimal? value)
		{
			bw.Write(value.HasValue);
			if (value.HasValue)
				bw.Write(value.Value);
		}
		public static void Write(this BinaryWriter bw, float? value)
		{
			bw.Write(value.HasValue);
			if (value.HasValue)
				bw.Write(value.Value);
		}
		public static void Write(this BinaryWriter bw, double? value)
		{
			bw.Write(value.HasValue);
			if (value.HasValue)
				bw.Write(value.Value);
		}
		public static void Write(this BinaryWriter bw, Guid? value)
		{
			bw.Write(value.HasValue);
			if (value.HasValue)
				bw.Write(value.Value);
		}
		public static void WriteNullable(this BinaryWriter bw, string value)
		{
			bw.Write(value != null);
			if (value != null)
				bw.Write(value);
		}
		public static void WriteNullableBinaryData(this BinaryWriter bw, byte[] value)
		{
			bw.Write(value != null);
			if (value != null)
				bw.WriteBinaryData(value);
		}
		public static void Write(this BinaryWriter bw, DateTime? value)
		{
			bw.Write(value.HasValue);
			if (value.HasValue)
				bw.Write(value.Value);
		}
		public static void Write(this BinaryWriter bw, TimeSpan? value)
		{
			bw.Write(value.HasValue);
			if (value.HasValue)
				bw.Write(value.Value);
		}
	}
}
