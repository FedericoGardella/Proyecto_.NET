using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using DotNetEnv;
using System.Text;
using BL.IBLs;
using BL.BLs;
using DAL.IDALs;
using DAL.DALs;

var builder = WebApplication.CreateBuilder(args);

// Configuración de cadena de conexión
string connectionString = "Server=sqlserver,1433;Database=master;User Id=sa;Password=P45w0rd.N3T;TrustServerCertificate=True";
Env.Load();


// Configuración de DbContext e Identity
builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));

builder.Services.AddIdentity<Users, IdentityRole>(options =>
{
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<DBContext>()
.AddDefaultTokenProviders();

// Inyección de dependencias
builder.Services.AddScoped<IBL_Pacientes, BL_Pacientes>();
builder.Services.AddScoped<IDAL_Pacientes, DAL_Pacientes_EF>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configuración de autenticación JWT
string? JWT_SECRET = Environment.GetEnvironmentVariable("JWT_SECRET");
string? JWT_ISSUER = Environment.GetEnvironmentVariable("JWT_ISSUER");
string? JWT_AUDIENCE = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

if (string.IsNullOrEmpty(JWT_SECRET) || string.IsNullOrEmpty(JWT_ISSUER) || string.IsNullOrEmpty(JWT_AUDIENCE))
{
    throw new InvalidOperationException("Las variables de entorno JWT_SECRET, JWT_ISSUER y JWT_AUDIENCE deben estar configuradas.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = JWT_AUDIENCE,
            ValidIssuer = JWT_ISSUER,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_SECRET)),
            ClockSkew = TimeSpan.FromMinutes(5),
        };
    });

// Servicios adicionales
builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Inserte el token JWT aquí",
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

// Configuración de tokens de protección de datos
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(2);
});

var app = builder.Build();

// Verificación de conexión a la base de datos y creación de roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<DBContext>();

    try
    {
        if (!dbContext.Database.CanConnect())
        {
            throw new Exception("No se pudo establecer conexión con la base de datos.");
        }

        Console.WriteLine("Conexión a la base de datos SQL Server exitosa.");

        string[] roles = { "USER", "ADMIN", "SUPERADMIN" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
        throw; // Detiene la aplicación si ocurre un error
    }
}

// Middleware de Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}
else
{
    Console.WriteLine("Swagger está deshabilitado en entornos de producción.");
}

// Middleware
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
