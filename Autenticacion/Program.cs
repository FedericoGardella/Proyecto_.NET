using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// DbContext and Identity
builder.Services.AddDbContext<DBContext>();
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

// Authentication
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

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(2);
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
