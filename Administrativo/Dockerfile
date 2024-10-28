# Usa la imagen base de .NET 8.0 SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Administrativo.csproj", "Administrativo/"]
RUN dotnet restore "Administrativo/Administrativo.csproj"
COPY . Administrativo/
WORKDIR "/src/Administrativo"
RUN dotnet build "Administrativo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Administrativo.csproj" -c Release -o /app/publish

# Usa la imagen base de .NET 8.0 Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Administrativo.dll"]
