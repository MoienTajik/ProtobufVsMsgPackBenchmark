using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using MessagePack;
using ProtoBuf;
using ProtobufVsMsgPack.Infrastructure.Data;
using ProtobufVsMsgPack.Infrastructure.Extensions;
using ProtobufVsMsgPack.Models;
using System.Collections.Generic;
using System.IO;

namespace ProtobufVsMsgPack.Benchmarks
{
    [Config(typeof(OutputSizeConfig))]
    public class ProtobufVsMsgPackSerializationBenchmark
    {
        private List<User> _users;

        [GlobalSetup]
        public void Setup()
        {
            _users = UsersRepository.GetUsers(); // About 10.5MB data
        }

        [Benchmark]
        public int MsgPackSerialize()
        {
            byte[] uncompressedBytesArray = MessagePackSerializer.Serialize(_users);
            byte[] gzipCompressedBytes = uncompressedBytesArray.GzipCompress();

            return gzipCompressedBytes.Length; // Serialized + Compressed Size
        }

        [Benchmark]
        public int ProtobufSerialize()
        {
            using var serializationStream = new MemoryStream();
            Serializer.Serialize(serializationStream, _users);

            byte[] uncompressedBytesArray = serializationStream.ToArray();
            byte[] gzipCompressedBytes = uncompressedBytesArray.GzipCompress();

            return gzipCompressedBytes.Length; // Serialized + Compressed Size
        }
    }

    public class OutputSizeConfig : ManualConfig
    {
        public OutputSizeConfig()
        {
            Add(new TagColumn("MsgPackOutputSize",
                _ =>
                {
                    var benchmark = new ProtobufVsMsgPackSerializationBenchmark();
                    benchmark.Setup();

                    return $"{benchmark.MsgPackSerialize()} bytes";
                }));

            Add(new TagColumn("ProtobufOutputSize",
                _ =>
                {
                    var benchmark = new ProtobufVsMsgPackSerializationBenchmark();
                    benchmark.Setup();

                    return $"{benchmark.ProtobufSerialize()} bytes";
                }));
        }
    }
}