using BenchmarkDotNet.StorageInMemory.DTOs;

namespace BenchmarkDotNet.StorageInMemory.Models;

public class BdnResult
{
    public string Title { get; set; }
    public HostEnvironmentInfo HostEnvironmentInfo { get; set; }
    public List<Benchmark> Benchmarks { get; set; }
    public GitVersion GitVersion { get; set; }
}