# Usar la imagen base de .NET SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Establecer el directorio de trabajo
WORKDIR /src

# Copiar solo el archivo de proyecto (csproj) y restaurar las dependencias
COPY ["Administrativo.csproj", "./"]


RUN dotnet restore "Administrativo.csproj"

# Copiar todos los archivos de la carpeta actual al contenedor
COPY . /src

# Compilar la aplicación en modo Release
RUN dotnet build "Administrativo.csproj" -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "Administrativo.csproj" -c Release -o /app/publish

# Usar la imagen base de .NET ASP.NET para el runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copiar los archivos publicados desde la etapa anterior
COPY --from=publish /app/publish .

# Establecer el punto de entrada para el contenedor
ENTRYPOINT ["dotnet", "Administrativo.dll"]
