using System.Text.Json;
using BenchmarkDotNet.StorageInMemory.Helpers;
using BenchmarkDotNet.StorageInMemory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BenchmarkDotNet.StorageInMemory.Controllers;

public class SvgController : Controller
{
    private readonly BenchmarkDotNetDb _db;
    
    public SvgController(BenchmarkDotNetDb db)
    {
        _db = db;
    }
    
    [Route("/svg/")]
    public async Task<IActionResult> Svg(string hostname, string fullname, int n = 6)
    {
        var benchmarks = await _db.Benchmarks
            .Include(b => b.BenchmarkAggregateResult)
            .Include(b => b.BenchmarkAggregateResult.GitVersion)
            .Where(b => b.FullName == fullname 
                        && b.BenchmarkAggregateResult.Hostname == hostname)
            .OrderByDescending(b => b.BenchmarkAggregateResult.Timestamp)
            .Take(n)
            .ToListAsync();
        // TakeLast() didn't work. not sure why.
        benchmarks.Reverse();

        var means = benchmarks.Select(b => b.Mean).ToList();
        var max = means.Max();
        var min = means.Min();
        var gitVersions = benchmarks
            .Select(b => b.BenchmarkAggregateResult.GitVersion)
            .ToList();

        var svgViewModel = new SvgViewModel
        {
            Benchmarks = benchmarks,
            GitVersions = gitVersions,
            Max = max,
            Min = min
        };
    
        var svg =
            await ViewRenderer.RenderViewToStringAsync("LineChart", svgViewModel, ControllerContext, true);
        
        return Content(svg, "image/svg+xml");
    }
}