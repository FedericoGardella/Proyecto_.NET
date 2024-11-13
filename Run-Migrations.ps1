# Definir el path del proyecto DAL
$dalProjectPath = "C:\Users\O012321\Desktop\dotNet\Proyecto_.NET\DAL"
# Definir el path de cada servicio
$serviceProjects = @(
    "Administrativo",
    "Autenticacion",
    "GestionCitas",
    "HistoriasClinicas",
    "PortalPaciente",
    "GestionUsuarios"
)

# Ruta de la carpeta obj (intermediarios de compilación) de DAL
$dalObjPath = "$dalProjectPath\obj"

# Ejecutar las migraciones para cada servicio
foreach ($service in $serviceProjects) {
    $servicePath = "C:\Users\O012321\Desktop\dotNet\Proyecto_.NET\$service"
    
    Write-Host "Ejecutando migración para el servicio $service..."
    
    # Ejecutar el comando de migración
    dotnet ef migrations add InitialCreate -p $dalProjectPath -s $servicePath --msbuildprojectextensionspath $dalObjPath
    # Aplicar la migración (si se ha creado correctamente)
    dotnet ef database update -p $dalProjectPath -s $servicePath --msbuildprojectextensionspath $dalObjPath

    Write-Host "Migración para $service completada."
}

Write-Host "¡Migraciones completas para todos los servicios!"
