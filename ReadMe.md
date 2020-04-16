### Steps to run:
1. Install .NET Core 3.1 SDK from here: https://dot.net/core
2. Execute `dotnet run` command in the root of the project.
3. See benchmark results  in `BenchmarkDotNet.Artifacts` folder.

<hr>

> BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
>  Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores 
>  .NET Core SDK=3.1.201  
>  [Host]: .NET Core 3.1.3 (CoreCLR 4.700.20.11803, CoreFX 4.700.20.12001), X64 RyuJIT 
> DefaultJob : .NET Core 3.1.3 (CoreCLR 4.700.20.11803, CoreFX 4.700.20.12001), X64 RyuJIT

**Serialize + GZip Compression:**
<table>
<thead><tr><th>     Method</th><th>Mean</th><th>Error</th><th>StdDev</th><th>Median</th><th>MsgPackOutputSize</th><th>ProtobufOutputSize</th>
</tr>
</thead><tbody><tr><td>MsgPackSerialize</td><td>48.14 ms</td><td>0.959 ms</td><td>2.260 ms</td><td>47.13 ms</td><td>381793 bytes</td><td>455954 bytes</td>
</tr><tr><td>ProtobufSerialize</td><td>81.24 ms</td><td>1.611 ms</td><td>4.041 ms</td><td>80.88 ms</td><td>381793 bytes</td><td>455954 bytes</td>
</tr></tbody></table>

<hr>

> BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
>  Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores 
>  .NET Core SDK=3.1.201  
>  [Host]: .NET Core 3.1.3 (CoreCLR 4.700.20.11803, CoreFX 4.700.20.12001), X64 RyuJIT 
> DefaultJob : .NET Core 3.1.3 (CoreCLR 4.700.20.11803, CoreFX 4.700.20.12001), X64 RyuJIT

**Deserialize + GZip Decompression:**
<table>
<thead><tr><th>       Method</th><th>Mean</th><th>Error</th><th>StdDev</th>
</tr>
</thead><tbody><tr><td>MsgPackDeserialize</td><td>85.66 ms</td><td>1.671 ms</td><td>2.172 ms</td>
</tr><tr><td>ProtobufDeserialize</td><td>107.33 ms</td><td>2.002 ms</td><td>2.142 ms</td>
</tr></tbody></table>