# BenchmarkDotNet.StorageInMemory

This .NET Minimal API project works with InMemory Entity Framework and Docker support. So users can deploy a working demo with minimal efforts and no worries regarding backing databases. How the JSON is stored and retrieved can greatly impact the API performance. But it's ignored for the sake of simplicity in this demo.

## Steps for recreating this project

1. use `dotnet new webapi -minimal` to create a new project without unnecessary packages and files.

    ```bash
    dotnet new webapi -minimal -o BenchmarkDotNet.StorageInMemory
    ```

2. Add data transfer objects (DTOs) based on BenchmarkDotNet JSON results using [JSON2CSharp](https://json2csharp.com/). I've extended the some classes to include additional properties such as hostname and gitversion infos.

3. Update `Program.cs` to include minimal API endpoints.