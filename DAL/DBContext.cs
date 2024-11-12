using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Shared.Entities;

namespace DAL
{
    public class DBContext : IdentityDbContext<Users>
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Si la configuración ya ha sido realizada en Program.cs, evita configurarla aquí
            if (!optionsBuilder.IsConfigured)
            {
                // Configura la cadena de conexión y resiliencia en caso de que no esté configurada
                optionsBuilder.UseSqlServer("Server=sqlserver,1433;Database=master;User Id=sa;Password=P45w0rd.N3T;TrustServerCertificate=True",
                    sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Articulo>()
                .Property(a => a.Costo)
                .HasPrecision(10, 2);

            // Configura la relación entre Articulos y TiposSeguros sin cascada
            modelBuilder.Entity<Articulos>()
                .HasOne(a => a.TiposSeguros)
                .WithMany(t => t.Articulos)
                .HasForeignKey(a => a.TiposSegurosId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configura la relación entre Articulos y PreciosEspecialidades sin cascada
            modelBuilder.Entity<Articulos>()
                .HasOne(a => a.PreciosEspecialidades)
                .WithMany() // Sin navegabilidad inversa en PreciosEspecialidades
                .HasForeignKey("PreciosEspecialidadesId") // Define la clave foránea directamente
                .OnDelete(DeleteBehavior.NoAction);

            // Configura la relación entre PreciosEspecialidades y TiposSeguros sin cascada
            modelBuilder.Entity<PreciosEspecialidades>()
                .HasOne(p => p.TiposSeguros)
                .WithMany()
                .HasForeignKey(p => p.TiposSegurosId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configura la relación entre PreciosEspecialidades y Especialidades sin cascada
            modelBuilder.Entity<PreciosEspecialidades>()
                .HasOne(p => p.Especialidades)
                .WithMany()
                .HasForeignKey(p => p.EspecialidadesId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configura la relación entre Citas y Pacientes
            modelBuilder.Entity<Citas>()
                .HasOne(c => c.Pacientes)
                .WithMany() // No navegabilidad inversa si no es necesaria
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.NoAction); // Evita cascada

            // Configura la relación entre Citas y GruposCitas
            modelBuilder.Entity<Citas>()
                .HasOne(c => c.GruposCitas)
                .WithMany(g => g.Citas)
                .HasForeignKey(c => c.GruposCitasId)
                .OnDelete(DeleteBehavior.NoAction); // Evita cascada para prevenir múltiples caminos

            // Configura la relación entre Citas y Facturas
            modelBuilder.Entity<Citas>()
                .HasOne(c => c.Facturas)
                .WithMany(f => f.Citas)
                .HasForeignKey(c => c.FacturasId)
                .OnDelete(DeleteBehavior.NoAction); // Evita cascada para prevenir múltiples caminos

            // Configura la relación entre Citas y PreciosEspecialidades
            modelBuilder.Entity<Citas>()
                .HasOne(c => c.PreciosEspecialidades)
                .WithMany() // Sin navegabilidad inversa
                .HasForeignKey(c => c.PreciosEspecialidadesId)
                .OnDelete(DeleteBehavior.NoAction); // Evita cascada para evitar conflictos

        }


        // Define tus DbSet para las entidades
        public DbSet<Articulos> Articulos { get; set; }
        public DbSet<ContratosSeguros> ContratosSeguros { get; set; }
        public DbSet<Diagnosticos> Diagnosticos { get; set; }
        public DbSet<Facturas> Facturas { get; set; }
        public DbSet<HistoriasClinicas> HistoriasClinicas { get; set; }
        public DbSet<Medicamentos> Medicamentos { get; set; }
        public DbSet<PreciosEspecialidades> PreciosEspecialidades { get; set; }
        public DbSet<Recetas> Recetas { get; set; }
        public DbSet<ResultadosEstudios> ResultadosEstudios { get; set; }
        public DbSet<Pacientes> Pacientes { get; set; }
        public DbSet<Especialidades> Especialidades { get; set; }
        public DbSet<Medicos> Medicos { get; set; }
        public DbSet<TiposSeguros> TiposSeguros { get; set; }
        public DbSet<Citas> Citas { get; set; }
        public DbSet<GruposCitas> GruposCitas { get; set; }
        public DbSet<Personas> Personas { get; set; }

        // Método para aplicar migraciones manualmente si es necesario
        public static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<DBContext>())
            {
                context.Database.Migrate();
            }
        }

    }
}

