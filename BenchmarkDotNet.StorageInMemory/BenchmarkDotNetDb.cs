using BenchmarkDotNet.StorageInMemory.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BenchmarkDotNet.StorageInMemory;

public class BenchmarkDotNetDb : DbContext
{
    public BenchmarkDotNetDb(DbContextOptions<BenchmarkDotNetDb> options)
        : base(options) { }

    public DbSet<BenchmarkDto> Benchmarks => Set<BenchmarkDto>();
    public DbSet<BenchmarkAggregateResultDto> BenchmarkAggregateResults => Set<BenchmarkAggregateResultDto>();
}