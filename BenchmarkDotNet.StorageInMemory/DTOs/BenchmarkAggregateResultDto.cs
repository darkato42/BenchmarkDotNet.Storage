using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BenchmarkDotNet.StorageInMemory.DTOs;

[Table("BenchmarkAggregateResult", Schema = "_app_BenchmarkDotNetStorage")]
public class BenchmarkAggregateResultDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Timestamp { get; set; }
    public string Hostname { get; set; }
    public string HostEnvironmentInfoJson { get; set; }
    public GitVersion GitVersion { get; set; }
    
    public string Json { get; set; }
    
    [JsonIgnore]
    public ICollection<BenchmarkDto> Benchmarks { get; set; }
}