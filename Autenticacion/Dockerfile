#Autenticacion

# Base de ASP.NET Core para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# SDK de .NET para compilar el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia los proyectos necesarios y restaura las dependencias
COPY ["DAL/DAL.csproj", "DAL/"]
COPY ["BL/BL.csproj", "BL/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["Autenticacion/Autenticacion.csproj", "Autenticacion/"]

RUN dotnet restore "./Autenticacion/Autenticacion.csproj"

# Copia el resto del código y construye el proyecto
COPY . .
WORKDIR "/src/Autenticacion"
RUN dotnet build "./Autenticacion.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publica el proyecto
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Autenticacion.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final para producción
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Autenticacion.dll"]