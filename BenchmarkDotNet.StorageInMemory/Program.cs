using System.Globalization;
using System.Text.Json;
using BenchmarkDotNet.StorageInMemory;
using BenchmarkDotNet.StorageInMemory.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BenchmarkDotNetDb>(opt => 
    opt.UseInMemoryDatabase("BenchmarkDotNetDb"));
var app = builder.Build();

app.MapGet("/", () => "Your BenchmarkDotNet.StorageInMemory API is live!");

// Save an aggregate BenchmarkDotNet result to the database
app.MapPost("/bdnresults", async (BdnResult bdnResult, BenchmarkDotNetDb db) =>
{
    // Take the last 15 from Title (e.g. 20210101-122020) as timestamp
    var dateTimeString = bdnResult.Title[^15..];
    var timestamp = DateTime.ParseExact(
        dateTimeString,
        "yyyyMMdd-HHmmss",
        CultureInfo.InvariantCulture);

    var benchmarks = bdnResult.Benchmarks.Select(b => new BenchmarkDto
    {
        Method = b.Method,
        FullName = b.FullName,
        Parameters = b.Parameters,
        Mean = b.Statistics.Mean,
        Median = b.Statistics.Median,
        StatsJson = JsonSerializer.Serialize(b.Statistics)
    }).ToList();

    // Add benchmark aggregate result
    var aggResultDto = new BenchmarkAggregateResultDto
    {
        Title = bdnResult.Title,
        Timestamp = timestamp,
        Hostname = bdnResult.HostEnvironmentInfo.Hostname,
        Json = JsonSerializer.Serialize(bdnResult),
        Benchmarks = benchmarks
    };
    db.BenchmarkAggregateResults.Add(aggResultDto);
    await db.SaveChangesAsync();
    var aggId = aggResultDto.Id;
    
    return Results.Created($"/bdnresults/{aggId}", aggResultDto);
});

// Get an aggregate BenchmarkDotNet result by id
app.MapGet("/bdnresults/{id}", async (int id, BenchmarkDotNetDb db) =>
    await db.BenchmarkAggregateResults.FindAsync(id)
        is BenchmarkAggregateResultDto benchmarkAggregateResultDto
        ? Results.Ok(benchmarkAggregateResultDto)
        : Results.NotFound());

// Get benchmarks by fullname and hostname
app.MapGet("/benchmarks", async (string fullname, string hostname, BenchmarkDotNetDb db) =>
{
    var benchmarks = await db.Benchmarks
        .Where(b => b.FullName == fullname && b.BenchmarkAggregateResult.Hostname == hostname)
        .ToListAsync<BenchmarkDto>();
    return Results.Ok(benchmarks);
});

app.Run();