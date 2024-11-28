using Microsoft.OpenApi.Models;
using DAL;
using DAL.Models;
using BL.IBLs;
using BL.BLs;
using DAL.IDALs;
using DAL.DALs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddScoped<IBL_Facturas, BL_Facturas>();
builder.Services.AddScoped<IDAL_Facturas, DAL_Facturas_EF>();
builder.Services.AddScoped<IBL_ContratosSeguros, BL_ContratosSeguros>();
builder.Services.AddScoped<IDAL_ContratosSeguros, DAL_ContratosSeguros_EF>();
builder.Services.AddScoped<IBL_Pacientes, BL_Pacientes>();
builder.Services.AddScoped<IDAL_Pacientes, DAL_Pacientes_EF>();
builder.Services.AddScoped<IBL_Articulos, BL_Articulos>();
builder.Services.AddScoped<IDAL_Articulos, DAL_Articulos_EF>();
builder.Services.AddScoped<IBL_PreciosEspecialidades, BL_PreciosEspecialidades>();
builder.Services.AddScoped<IDAL_PreciosEspecialidades, DAL_PreciosEspecialidades_EF>();
builder.Services.AddScoped<IBL_Especialidades, BL_Especialidades>();
builder.Services.AddScoped<IDAL_Especialidades, DAL_Especialidades_Service>(); // Cambiar cuando no sea mock
builder.Services.AddScoped<IBL_PreciosEspecialidades, BL_PreciosEspecialidades>();
builder.Services.AddScoped<IDAL_PreciosEspecialidades, DAL_PreciosEspecialidades_EF>();
builder.Services.AddScoped<IBL_FacturasMes,  BL_FacturasMes>();
builder.Services.AddScoped<IDAL_FacturasMes,  DAL_FacturasMes_Service>();


// Configuración para escuchar en todas las interfaces
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // Escucha en el puerto 80 en todas las interfaces
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

// Agrega los servicios de controladores
builder.Services.AddControllers();

// Configura Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Administrativo API", Version = "v1" });
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
            new string[] {}
        }
    });
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
app.UseCors("AllowAll");

// Habilita el uso de autenticación y autorización
app.UseAuthentication();

app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
