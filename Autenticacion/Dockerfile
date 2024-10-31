FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia el archivo .csproj y restaura las dependencias
COPY ["Autenticacion.csproj", "./"]
RUN dotnet restore "Autenticacion.csproj"

# Copia el resto de los archivos del proyecto
COPY . .

# Compila el proyecto
WORKDIR "/src"
RUN dotnet build "Autenticacion.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Publica el proyecto
RUN dotnet publish "Autenticacion.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

# Copia los archivos publicados al contenedor final
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Autenticacion.dll"]
