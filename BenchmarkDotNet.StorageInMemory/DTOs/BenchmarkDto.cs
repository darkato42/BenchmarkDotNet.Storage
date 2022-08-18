using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BenchmarkDotNet.StorageInMemory.DTOs;

[Table("Benchmark", Schema = "_app_BenchmarkDotNetStorage")]
public class BenchmarkDto
{
    public int Id { get; set; }
    public int BenchmarkAggregateResultId { get; set; }
    [JsonIgnore]
    public BenchmarkAggregateResultDto BenchmarkAggregateResult { get; set; }
    
    // Only storing some key properties for now.
    public string Method { get; set; }
    public string FullName { get; set; }
    public string Parameters { get; set; }
    public double Mean { get; set; }
    public double Median { get; set; }
    public string StatsJson { get; set; }
}