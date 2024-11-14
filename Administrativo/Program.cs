using Microsoft.OpenApi.Models;
using DAL;
using DAL.Models;
using BL.IBLs;
using BL.BLs;
using DAL.IDALs;
using DAL.DALs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura la cadena de conexión para DBContext
string connectionString = "Server=sqlserver,1433;Database=master;User Id=sa;Password=P45w0rd.N3T;TrustServerCertificate=True";
builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(
        connectionString,
        sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
    ));


// Registrar servicios en el contenedor de inyección de dependencias
builder.Services.AddScoped<IBL_TiposSeguros, BL_TiposSeguros>();
builder.Services.AddScoped<IDAL_TiposSeguros, DAL_TiposSeguros_EF>();

// Configuración para escuchar en todas las interfaces
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // Escucha en el puerto 80 en todas las interfaces
});

// Agrega los servicios
builder.Services.AddControllers();

// Configura Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Administrativo API", Version = "v1" });
});

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Cambia esta URL al origen de tu frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configura el middleware de Swagger en el entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Administrativo API V1");
    });
}

// Aplica la política de CORS
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
