# Dockerfile for .NET 9 Web API (Multi-stage Build)

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia el csproj y restaura
COPY ["minicore-comiciones.csproj", "./"]
RUN dotnet restore "minicore-comiciones.csproj"

# Copia el resto del código y publica
COPY . .
RUN dotnet publish "minicore-comiciones.csproj" -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copia los artefactos publicados
COPY --from=build /app/publish .

# Expone el puerto 80 para HTTP
EXPOSE 80

# Arranca la aplicación
ENTRYPOINT ["dotnet", "minicore-comiciones.dll"]
