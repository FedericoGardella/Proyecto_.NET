using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

            modelBuilder.Entity<Articulos>()
                .Property(a => a.Costo)
                .HasPrecision(10, 2);

            // Configura la relación entre Articulos y TiposSeguros sin cascada
            modelBuilder.Entity<Articulos>()
                .HasOne(a => a.TiposSeguros)
                .WithMany() // No hay navegabilidad inversa desde TiposSeguros hacia Articulos
                .HasForeignKey(a => a.TiposSegurosId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relación entre PreciosEspecialidades y Articulos
            modelBuilder.Entity<PreciosEspecialidades>()
                .HasOne(pe => pe.Articulos)
                .WithMany() // Sin navegabilidad inversa desde Articulos hacia PreciosEspecialidades
                .HasForeignKey(pe => pe.ArticulosId)
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

            modelBuilder.Entity<Pacientes>()
                .HasOne(p => p.HistoriasClinicas)
                .WithOne(h => h.Pacientes) // Configuración en ambos lados de la relación
                .HasForeignKey<Pacientes>(p => p.HistoriasClinicasId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Diagnosticos>()
                .HasOne(d => d.HistoriasClinicas)
                .WithMany(h => h.Diagnosticos)
                .HasForeignKey(d => d.HistoriasClinicasId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre Pacientes y ContratosSeguros (uno a muchos)
            modelBuilder.Entity<ContratosSeguros>()
                .HasOne(cs => cs.Pacientes)
                .WithMany(p => p.ContratosSeguros)
                .HasForeignKey(cs => cs.PacientesId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre ContratosSeguros y TiposSeguros
            modelBuilder.Entity<ContratosSeguros>()
                .HasOne(cs => cs.TiposSeguros)
                .WithMany() // Asume que TiposSeguros no tiene una colección de ContratosSeguros
                .HasForeignKey(cs => cs.TiposSegurosId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre Articulos y Especialidades
            modelBuilder.Entity<Articulos>()
                .HasOne(a => a.Especialidades)
                .WithMany() // No hay navegabilidad inversa desde Especialidades hacia Articulos
                .HasForeignKey(a => a.EspecialidadesId)
                .OnDelete(DeleteBehavior.NoAction);

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
