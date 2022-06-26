#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DockerContainer_Kafka_Demo/DockerContainer_Kafka_Demo.csproj", "DockerContainer_Kafka_Demo/"]
RUN dotnet restore "DockerContainer_Kafka_Demo/DockerContainer_Kafka_Demo.csproj"
COPY . .
WORKDIR "/src/DockerContainer_Kafka_Demo"
RUN dotnet build "DockerContainer_Kafka_Demo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DockerContainer_Kafka_Demo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerContainer_Kafka_Demo.dll"]
