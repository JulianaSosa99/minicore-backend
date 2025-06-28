### Dockerfile for .NET 9 Web API (Multi-stage Build)
```dockerfile
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY *.sln .
COPY YourProjectFolder/*.csproj ./YourProjectFolder/
RUN dotnet restore

# Copy rest of source code and publish
COPY . .
WORKDIR /src/YourProjectFolder
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Expose port (the port your API listens on)
EXPOSE 80

# Entry point: replace YourProject.dll with your actual DLL name
ENTRYPOINT ["dotnet", "YourProjectFolder.dll"]
