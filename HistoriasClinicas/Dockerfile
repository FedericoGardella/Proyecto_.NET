# Usar la imagen base para ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Usar la imagen SDK de .NET para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar el archivo de proyecto y restaurar las dependencias
COPY ["HistoriasClinicas.csproj", "./"]
RUN dotnet restore "HistoriasClinicas.csproj"


# Copia el resto de los archivos del proyecto
COPY . .

# Compila el proyecto
WORKDIR "/src"

RUN dotnet build "HistoriasClinicas.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicar la aplicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HistoriasClinicas.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Usar la imagen base en la fase final
FROM base AS final
WORKDIR /app

# Copiar los archivos publicados a la imagen final
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "HistoriasClinicas.dll"]
