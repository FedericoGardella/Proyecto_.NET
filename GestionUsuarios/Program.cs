using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DAL;
using BL.IBLs;
using BL.BLs;
using DAL.IDALs;
using DAL.DALs;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la cadena de conexión para DBContext
string connectionString = "Server=sqlserver,1433;Database=GestionUsuariosDB;User Id=sa;Password=P45w0rd.N3T;TrustServerCertificate=True";
builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(
        connectionString,
        sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
    ));

// Registro de dependencias para inyección de dependencias
builder.Services.AddScoped<IBL_Especialidades, BL_Especialidades>();
builder.Services.AddScoped<IDAL_Especialidades, DAL_Especialidades_Service>();

// Configuración para escuchar en todas las interfaces
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // Escucha en el puerto 8086 para este servicio
});

// Configuración de autenticación JWT
string? JWT_SECRET = Environment.GetEnvironmentVariable("JWT_SECRET");
string? JWT_ISSUER = Environment.GetEnvironmentVariable("JWT_ISSUER");
string? JWT_AUDIENCE = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

Console.WriteLine($"JWT_SECRET: {JWT_SECRET}");
Console.WriteLine($"JWT_ISSUER: {JWT_ISSUER}");
Console.WriteLine($"JWT_AUDIENCE: {JWT_AUDIENCE}");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = JWT_AUDIENCE,
        ValidIssuer = JWT_ISSUER,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_SECRET)),
        ClockSkew = TimeSpan.Zero
    };
});

// Agrega servicios de autorización
builder.Services.AddAuthorization();

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

// Configuración de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GestionUsuarios API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Inserte el token JWT aquí usando el formato 'Bearer {token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

// Configuración de servicios y controladores
builder.Services.AddControllers();

var app = builder.Build();

// Configuración del middleware de Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestionUsuarios API V1");
    });
}

app.UseCors("AllowAll");

// Habilita el uso de autenticación y autorización
app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
