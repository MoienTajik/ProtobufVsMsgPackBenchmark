using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace ProtobufVsMsgPack
{
    internal static class Program
    {
        // Use this constructor if you want to debug benchmarks
        private static void Main()
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
                .Run(null, new DebugInProcessConfig());
        }

        // Use this constructor if you want to get benchmarks - Switch to Release mode with this constructor
        //private static void Main()
        //{
        //    Summary serializationSummary = BenchmarkRunner.Run<ProtobufVsMsgPackSerializationBenchmark>();
        //    Summary deserializationSummary = BenchmarkRunner.Run<ProtobufVsMsgPackDeserializationBenchmark>();
        //}
    }
}