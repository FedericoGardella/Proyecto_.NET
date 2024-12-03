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

// Configura Swagger para usar la autenticación JWT
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

// Registrar servicios necesarios (ejemplo de servicios específicos para este API)
builder.Services.AddScoped<IBL_Citas, BL_Citas>();
builder.Services.AddScoped<IDAL_Citas, DAL_Citas_EF>();

builder.Services.AddScoped<IBL_GruposCitas, BL_GruposCitas>();
builder.Services.AddScoped<IDAL_GruposCitas, DAL_GruposCitas_EF>();

builder.Services.AddScoped<IBL_Pacientes, BL_Pacientes>();
builder.Services.AddHttpClient<IDAL_Pacientes, DAL_Pacientes_Service>();

// Registrar el servicio en segundo plano para recordatorios
builder.Services.AddHostedService<ReminderService>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();



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
