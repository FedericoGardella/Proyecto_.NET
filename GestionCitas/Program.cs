using BL.BLs;
using BL.IBLs;
using DAL;
using DAL.DALs;
using DAL.IDALs;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
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

builder.Services.AddScoped<IBL_Citas, BL_Citas>();
builder.Services.AddScoped<IDAL_Citas, DAL_Citas_EF>();
builder.Services.AddScoped<IDAL_Citas, DAL_Citas_EF>();

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


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
