# Library to help working with binary data.

## Features

The main features currently are support of BinaryReader and BinaryWriter extensions. These include extension methods that write nullable data, as well as filling in a few standard types that aren't included by default.

## In progress

Currently there is work being done to support an aynchronous BinaryReader and BinaryWriter. This would work by buffering the binary data in a MemoryStream and then dumping it into the underlying stream asynchronously in a batch.

## Remarks

Currently there is no support for Span<T> and Memory<T> (or the readonly variaties). This would be nice to have, however currently the library is supporting older .Net Framework versions so this doesn't likely to come soon.