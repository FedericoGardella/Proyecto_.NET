FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["GestionCitas.csproj", "./"]

RUN dotnet restore "GestionCitas.csproj"

# Copia el resto de los archivos del proyecto
COPY . .

# Compila el proyecto
WORKDIR "/src"
RUN dotnet build "GestionCitas.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "GestionCitas.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestionCitas.dll"]
