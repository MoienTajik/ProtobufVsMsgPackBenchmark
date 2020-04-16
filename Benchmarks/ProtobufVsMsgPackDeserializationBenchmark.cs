using BenchmarkDotNet.Attributes;
using MessagePack;
using ProtoBuf;
using ProtobufVsMsgPack.Infrastructure.Data;
using ProtobufVsMsgPack.Infrastructure.Extensions;
using ProtobufVsMsgPack.Models;
using System.Collections.Generic;
using System.IO;

namespace ProtobufVsMsgPack.Benchmarks
{
    public class ProtobufVsMsgPackDeserializationBenchmark
    {
        private byte[] _msgPackSerializedResponse;
        private byte[] _protobufSerializedResponse;

        [GlobalSetup]
        public void Setup()
        {
            List<User> searchResponses = UsersRepository.GetUsers();

            // Serialize using MsgPack & Compress with GZip
            _msgPackSerializedResponse = MessagePackSerializer
                .Serialize(searchResponses)
                .GzipCompress();

            // Serialize using Protobuf & Compress with GZip
            using var stream = new MemoryStream();
            Serializer.Serialize(stream, searchResponses);

            byte[] uncompressedBytesArray = stream.ToArray();
            _protobufSerializedResponse = uncompressedBytesArray.GzipCompress();
        }

        [Benchmark]
        public List<User> MsgPackDeserialize()
        {
            byte[] uncompressedBytes = _msgPackSerializedResponse.GzipDecompress();
            var searchResponses = MessagePackSerializer.Deserialize<List<User>>(uncompressedBytes);
            
            return searchResponses;
        }

        [Benchmark]
        public List<User> ProtobufDeserialize()
        {
            byte[] uncompressedBytes = _protobufSerializedResponse.GzipDecompress();
            using var deserializationStream = new MemoryStream(uncompressedBytes);

            return Serializer.Deserialize<List<User>>(deserializationStream);
        }
    }
}