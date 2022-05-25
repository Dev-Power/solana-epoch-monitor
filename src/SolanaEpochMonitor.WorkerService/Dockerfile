FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SolanaEpochMonitor.WorkerService/SolanaEpochMonitor.WorkerService.csproj", "SolanaEpochMonitor.WorkerService/"]
RUN dotnet restore "SolanaEpochMonitor.WorkerService/SolanaEpochMonitor.WorkerService.csproj"
COPY . .
WORKDIR "/src/SolanaEpochMonitor.WorkerService"
RUN dotnet build "SolanaEpochMonitor.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SolanaEpochMonitor.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SolanaEpochMonitor.WorkerService.dll"]
