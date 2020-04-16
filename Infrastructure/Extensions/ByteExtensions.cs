using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using K4os.Compression.LZ4;

namespace ProtobufVsMsgPack.Infrastructure.Extensions
{
    public static class ByteExtensions
    {
        public static byte[] GzipCompress(this byte[] uncompressedBytesArray)
        {
            using var compressionStream = new MemoryStream();
            byte[] bytesLength = BitConverter.GetBytes(uncompressedBytesArray.Length);
            compressionStream.Write(bytesLength, 0, 4);

            using (var gZipCompressionStream = new GZipStream(compressionStream, CompressionMode.Compress))
            {
                gZipCompressionStream.Write(uncompressedBytesArray, 0, uncompressedBytesArray.Length);
                gZipCompressionStream.Flush();
            }

            return compressionStream.ToArray();
        }

        public static byte[] GzipDecompress(this byte[] bytesToDecompress)
        {
            using var decompressionStream = new MemoryStream(bytesToDecompress);

            var bytesLength = new byte[4];
            decompressionStream.Read(bytesLength, 0, 4);

            using var gZipDecompressionStream = new GZipStream(decompressionStream, CompressionMode.Decompress);

            var decompressedBytesLength = BitConverter.ToInt32(bytesLength, 0);
            var uncompressedBytes = new byte[decompressedBytesLength];

            gZipDecompressionStream.Read(uncompressedBytes, 0, decompressedBytesLength);

            return uncompressedBytes;
        }

        public static byte[] Lz4Compress(this byte[] uncompressedBytesArray)
        {
            var target = new byte[LZ4Codec.MaximumOutputSize(uncompressedBytesArray.Length)];

            int compressedBytesSize = LZ4Codec.Encode(
                uncompressedBytesArray, 0, uncompressedBytesArray.Length,
                target, 0, target.Length,
                LZ4Level.L12_MAX);

            byte[] actualBytesAfterCompression = target.Take(compressedBytesSize).ToArray();

            return actualBytesAfterCompression;
        }

        public static byte[] Lz4Decompress(this byte[] bytesToDecompress)
        {
            // Maybe not so accurate, measurement on this is on your own.
            const int maximumExpectedLengthAfterDeserialize = 300;

            var target = new byte[bytesToDecompress.Length * maximumExpectedLengthAfterDeserialize];
            int decoded = LZ4Codec.Decode(
                bytesToDecompress, 0, bytesToDecompress.Length,
                target, 0, target.Length);

            byte[] uncompressedBytes = target.Take(decoded).ToArray();

            return uncompressedBytes;
        }

        public static byte[] BrotliCompress(this byte[] uncompressedBytesArray)
        {
            using var compressionStream = new MemoryStream();
            byte[] bytesLength = BitConverter.GetBytes(uncompressedBytesArray.Length);
            compressionStream.Write(bytesLength, 0, 4);

            using (var brotliCompressionStream = new BrotliStream(
                compressionStream, CompressionMode.Compress, false))
            {
                brotliCompressionStream.Write(uncompressedBytesArray, 0, uncompressedBytesArray.Length);
                brotliCompressionStream.Flush();
            }

            return compressionStream.ToArray();
        }

        public static byte[] BrotliDecompress(this byte[] bytesToDecompress)
        {
            using var decompressionStream = new MemoryStream(bytesToDecompress);
            var bytesLength = new byte[4];
            decompressionStream.Read(bytesLength, 0, 4);

            using var brotliDecompressionStream = new BrotliStream(
                decompressionStream, CompressionMode.Decompress, false);
          
            var decompressedBytesLength = BitConverter.ToInt32(bytesLength, 0);
            var uncompressedBytes = new byte[decompressedBytesLength];

            brotliDecompressionStream.Read(uncompressedBytes, 0, decompressedBytesLength);

            return uncompressedBytes;
        }
    }
}