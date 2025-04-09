# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS base
WORKDIR /accident-monitor-app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
COPY ["./*.props", "./"]
COPY ["./*.sln", "./"]
COPY ["./src/Domain/Domain.csproj", "./src/Domain/"]
COPY ["./src/Application/Application.csproj", "./src/Application/"]
COPY ["./src/Infrastructure/Infrastructure.csproj", "./src/Infrastructure/"]
COPY ["./src/ServiceDefaults/ServiceDefaults.csproj", "./src/ServiceDefaults/"]
COPY ["./src/Web/Web.csproj", "./src/Web/"]
COPY ["./src/AppHost/AppHost.csproj", "./src/AppHost/"]

COPY ["./tests/Application.FunctionalTests/Application.FunctionalTests.csproj", "./tests/Application.FunctionalTests/"]
COPY ["./tests/Application.UnitTests/Application.UnitTests.csproj", "./tests/Application.UnitTests/"]
COPY ["./tests/Domain.UnitTests/Domain.UnitTests.csproj", "./tests/Domain.UnitTests/"]
COPY ["./tests/Infrastructure.IntegrationTests/Infrastructure.IntegrationTests.csproj", "./tests/Infrastructure.IntegrationTests/"]

RUN dotnet restore "AccidentMonitor.sln"
COPY [".", "./"]

WORKDIR "/accident-monitor-app/src/Web"

# List the contents of the current directory for debugging
RUN ls -la /accident-monitor-app/src/Web

# List the contents of the src directory for debugging
RUN ls -la /accident-monitor-app/src

RUN dotnet build "./src/Web/Web.csproj" -c Release -o /accident-monitor-app/publish

FROM build AS publish
RUN dotnet publish "./src/Web/Web.csproj" -c Release -o /accident-monitor-app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /accident-monitor-app/publish .

# Set entry point
ENTRYPOINT ["dotnet", "AccidentMonitor.Web.dll"]
