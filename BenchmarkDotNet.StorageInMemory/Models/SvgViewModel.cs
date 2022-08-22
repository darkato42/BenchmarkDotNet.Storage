using BenchmarkDotNet.StorageInMemory.DTOs;

namespace BenchmarkDotNet.StorageInMemory.Models;

public class SvgViewModel
{
    public List<BenchmarkDto> Benchmarks { get; set; }
    public List<GitVersion> GitVersions { get; set; }
    public double Max { get; set; }
    public double Min { get; set; }
    public double Range => (Max - Min) * 1.1;
    public double SvgHeight = 300.0;
    public double SvgWidth = 600.0;
    public double YMin => SvgHeight * 0.75;
    public double YZero => SvgHeight * 0.8;
    public double XEnd => SvgWidth * 0.8;
    public int Count => Benchmarks.Count;

    public double XPos(int x) => XEnd / (Count - 1) * x;
    public double YPos(double value) => YMin - (value - Min) / Range * YMin;
    
    public string PointList => 
        string.Join(" ", Benchmarks.Select((b, i) => $"{XPos(i)} {YPos(b.Mean)}"));
    public List<(int, double, double)> Points => 
        Benchmarks.Select((b, i) => (i, XPos(i), YPos(b.Mean)))
            .ToList();
}