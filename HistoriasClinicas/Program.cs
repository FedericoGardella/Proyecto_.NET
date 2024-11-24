using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using BL.BLs;
using BL.IBLs;
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
        Title = "Historias Clinicas API",
        Description = "API para gestionar historias clínicas"
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

builder.Services.AddScoped<IBL_HistoriasClinicas, BL_HistoriasClinicas>();
builder.Services.AddScoped<IDAL_HistoriasClinicas, DAL_HistoriasClinicas_EF>();

builder.Services.AddScoped<IBL_Diagnosticos, BL_Diagnosticos>();
builder.Services.AddScoped<IDAL_Diagnosticos, DAL_Diagnosticos_EF>();

builder.Services.AddScoped<IBL_ResultadosEstudios, BL_ResultadosEstudios>();
builder.Services.AddScoped<IDAL_ResultadosEstudios, DAL_ResultadosEstudios_EF>();

builder.Services.AddScoped<IBL_Recetas, BL_Recetas>();
builder.Services.AddScoped<IDAL_Recetas, DAL_Recetas_EF>();

builder.Services.AddScoped<IBL_Medicamentos, BL_Medicamentos>();
builder.Services.AddScoped<IDAL_Medicamentos, DAL_Medicamentos_EF>();

builder.Services.AddScoped<IBL_GruposCitas, BL_GruposCitas>();
builder.Services.AddScoped<IDAL_GruposCitas, DAL_GruposCitas_Service>();

builder.Services.AddScoped<IBL_Pacientes, BL_Pacientes>();
builder.Services.AddScoped<IDAL_Pacientes, DAL_Pacientes_Service>();


var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Historias Clinicas API v1");
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
