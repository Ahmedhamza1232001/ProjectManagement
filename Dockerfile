# ============================
# 1. Build stage
# ============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and restore
COPY ProjectManagement.sln ./
COPY src/ProjectManagement.Api/ProjectManagement.Api.csproj src/ProjectManagement.Api/
COPY src/ProjectManagement.Application/ProjectManagement.Application.csproj src/ProjectManagement.Application/
COPY src/ProjectManagement.Domain/ProjectManagement.Domain.csproj src/ProjectManagement.Domain/
COPY src/ProjectManagement.Infrastructure/ProjectManagement.Infrastructure.csproj src/ProjectManagement.Infrastructure/
COPY tests/ProjectManagement.UnitTests/ProjectManagement.UnitTests.csproj tests/ProjectManagement.UnitTests/
COPY tests/ProjectManagement.IntegrationTests/ProjectManagement.IntegrationTests.csproj tests/ProjectManagement.IntegrationTests/
RUN dotnet restore

# Copy everything and build
COPY . .
WORKDIR /src/src/ProjectManagement.Api
RUN dotnet publish -c Release -o /app/publish

# ============================
# 2. Runtime stage
# ============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose port
EXPOSE 5000

# Run API
ENTRYPOINT ["dotnet", "ProjectManagement.Api.dll"]
