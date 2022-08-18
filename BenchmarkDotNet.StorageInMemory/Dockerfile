FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BenchmarkDotNet.StorageInMemory.csproj", "."]
RUN dotnet restore "BenchmarkDotNet.StorageInMemory.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "BenchmarkDotNet.StorageInMemory.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BenchmarkDotNet.StorageInMemory.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BenchmarkDotNet.StorageInMemory.dll"]
