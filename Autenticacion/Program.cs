using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BL.IBLs;
using BL.BLs;
using DAL.IDALs;
using DAL.DALs;

var builder = WebApplication.CreateBuilder(args);

// Configura la cadena de conexión a la base de datos
string connectionString = "Server=localhost,1433;Database=master;User Id=sa;Password=P45w0rd.N3T;TrustServerCertificate=True";

// DbContext and Identity
builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(
        connectionString,
        sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)  // Habilita resiliencia
    ));
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

builder.Services.AddScoped<IBL_Pacientes, BL_Pacientes>();
builder.Services.AddScoped<IDAL_Pacientes, DAL_Pacientes_EF>();

builder.Services.AddScoped<IBL_Personas, BL_Personas>();
builder.Services.AddScoped<IDAL_Personas, DAL_Personas_EF>();

builder.Services.AddScoped<IBL_Especialidades, BL_Especialidades>();
builder.Services.AddScoped<IDAL_Especialidades, DAL_Especialidades_Service>();

builder.Services.AddScoped<IBL_Medicos, BL_Medicos>();
builder.Services.AddScoped<IDAL_Medicos, DAL_Medicos_EF>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configuración de autenticación JWT
string? JWT_SECRET = Environment.GetEnvironmentVariable("JWT_SECRET");
string? JWT_ISSUER = Environment.GetEnvironmentVariable("JWT_ISSUER");
string? JWT_AUDIENCE = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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

// Add Authorization Service
builder.Services.AddAuthorization();

// Add Controllers Service
builder.Services.AddControllers();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insert JWT token",
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

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(2);
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

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Users>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<DBContext>();

    // Verifica conexión a la base de datos
    try
    {
        if (dbContext.Database.CanConnect())
            Console.WriteLine("Conexión a la base de datos SQL Server exitosa.");
        else
            Console.WriteLine("No se pudo establecer conexión con la base de datos.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
    }

    // Crea roles
    string[] roles = { "PACIENTE", "MEDICO", "ADMIN" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    var personaAdmin = await dbContext.Personas.FirstOrDefaultAsync(p => p.Documento == "00000000");

    // Si no existe, crea la entidad Persona
    if (personaAdmin == null)
    {
        personaAdmin = new Personas
        {
            Nombres = "Admin",
            Apellidos = "User",
            Documento = "00000000",
        };
        dbContext.Personas.Add(personaAdmin);
        await dbContext.SaveChangesAsync();
    }

    // Crear usuario administrador si no existe
    var adminEmail = "admin@example.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        var admin = new Users
        {
            UserName = "admin",
            Email = adminEmail,
            EmailConfirmed = true,
            Activo = true,
            PersonasId = personaAdmin.Id, // Asigna el ID de Personas
            Personas = personaAdmin // Asigna la entidad Personas completa
        };

        var createUserResult = await userManager.CreateAsync(admin, "Abc*123!");
        if (createUserResult.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "ADMIN");
            Console.WriteLine("Usuario administrador creado exitosamente.");
        }
        else
        {
            Console.WriteLine("Error al crear el usuario administrador:");
            foreach (var error in createUserResult.Errors)
            {
                Console.WriteLine($"- {error.Description}");
            }
        }
    }
    else
    {
        Console.WriteLine("Usuario administrador ya existe.");
    }
}

// Swagger Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}


// Aplica la política de CORS
//app.UseCors("AllowFrontend");

// Configuración del middleware de CORS
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
