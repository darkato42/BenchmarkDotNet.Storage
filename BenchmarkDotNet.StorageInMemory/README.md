# BenchmarkDotNet.StorageInMemory

This .NET Minimal API project works with InMemory Entity Framework and Docker support. So users can deploy a working demo with minimal efforts and no worries regarding backing databases. How the JSON is stored and retrieved can greatly impact the API performance. But it's ignored for the sake of simplicity in this demo.

## Steps for recreating this project

1. use `dotnet new webapi -minimal` to create a new project without unnecessary packages and files.

    ```bash
    dotnet new webapi -minimal -o BenchmarkDotNet.StorageInMemory
    ```

2. Add data transfer objects (DTOs) based on BenchmarkDotNet JSON results using [JSON2CSharp](https://json2csharp.com/). I've extended the some classes to include additional properties such as hostname and gitversion infos.
3. Update `Program.cs` to include minimal API endpoints.
4. Run/Debug with the profiles in `launchSettings.json`.

## Add Docker Support

1. Add Docker support in IDE.
2. Run `docker build -t benchmarkdotnet.storageinmemory .` to build the Docker image. tag only allows lowercase letters.
3. Run `docker run -p 8080:80 benchmarkdotnet.storageinmemory` to run the Docker image.
4. Share to Docker Hub.
   ```bash
   docker tag benchmarkdotnet.storageinmemory darkato/benchmarkdotnet.storageinmemory:1.0
   docker push darkato/benchmarkdotnet.storageinmemory:1.0
   ```

## Run docker image from Docker Hub

```bash
docker run -p 8080:80 darkato/benchmarkdotnet.storageinmemory:1.0
```

## Test the API

The greeting endpoint is hosted at http://localhost:8080.

1. Install **Thunder Client** in Visual Studio Code.
2. Import `thunder-collection_BenchmarkDotNet.StorageInMemory.json` into **Thunder Client** collections.
3. Send the test HTTP requests.