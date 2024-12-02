using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using DAL;
using BL.IBLs;
using BL.BLs;
using DAL.IDALs;
using DAL.DALs;

var builder = WebApplication.CreateBuilder(args);

// Configura la cadena de conexión a la base de datos
string connectionString = "Server=sqlserver,1433;Database=master;User Id=sa;Password=P45w0rd.N3T;TrustServerCertificate=True";

// Configuración de DbContext
builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(
        connectionString,
        sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
    ));

// Configuración para escuchar en el puerto 8080 interno (mapeado a 8081 en el contenedor Docker)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080); // Escucha en el puerto 8080 dentro del contenedor
});

// Configura la autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")))
        };
    });

// Configura CORS para permitir todos los orígenes
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Nuevo Servicio API",
        Description = "API para gestionar el nuevo servicio"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT en el formato 'Bearer {token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// Agrega servicios de controladores
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Registrar servicios en el contenedor de inyección de dependencias
builder.Services.AddScoped<IBL_TiposSeguros, BL_TiposSeguros>();
builder.Services.AddScoped<IDAL_TiposSeguros, DAL_TiposSeguros_EF>();
builder.Services.AddScoped<IBL_Facturas, BL_Facturas>();
builder.Services.AddScoped<IDAL_Facturas, DAL_Facturas_EF>();
builder.Services.AddScoped<IBL_ContratosSeguros, BL_ContratosSeguros>();
builder.Services.AddScoped<IDAL_ContratosSeguros, DAL_ContratosSeguros_EF>();
builder.Services.AddScoped<IBL_Pacientes, BL_Pacientes>();
builder.Services.AddHttpClient<IDAL_Pacientes, DAL_Pacientes_Service>();
builder.Services.AddScoped<IBL_Articulos, BL_Articulos>();
builder.Services.AddScoped<IDAL_Articulos, DAL_Articulos_EF>();
builder.Services.AddScoped<IBL_PreciosEspecialidades, BL_PreciosEspecialidades>();
builder.Services.AddScoped<IDAL_PreciosEspecialidades, DAL_PreciosEspecialidades_EF>();
builder.Services.AddScoped<IBL_Especialidades, BL_Especialidades>();
builder.Services.AddScoped<IDAL_Especialidades, DAL_Especialidades_EF>();
builder.Services.AddScoped<IBL_FacturasMes, BL_FacturasMes>();
builder.Services.AddScoped<IDAL_FacturasMes, DAL_FacturasMes_Service>();


var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nuevo Servicio API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

// Middleware de CORS
app.UseCors("AllowAll");

// Agrega el middleware de enrutamiento antes de autenticación y autorización
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();