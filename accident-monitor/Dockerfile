# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /accident-monitor-app
EXPOSE 80
EXPOSE 443

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

RUN dotnet restore "AccidentMonitoring.sln"

COPY [".", "./"]

WORKDIR "/accident-monitor-app/src/Web"
RUN dotnet publish "Web.csproj" -c Release -o /accident-monitor-app/publish
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=build /accident-monitor-app/publish .

# Set entry point
ENTRYPOINT ["dotnet", "AccidentMonitoring.Web.dll"]
