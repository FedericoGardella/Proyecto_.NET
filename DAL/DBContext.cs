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
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=master;User Id=sa;Password=P45w0rd.N3T;TrustServerCertificate=True",
                    sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Articulos>()
                .Property(a => a.Costo)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PreciosEspecialidades>()
                .HasOne(pe => pe.Articulos) // PreciosEspecialidades tiene un Articulo relacionado
                .WithOne(a => a.PreciosEspecialidades) // Articulo tiene un PreciosEspecialidades relacionado
                .HasForeignKey<PreciosEspecialidades>(pe => pe.ArticulosId) // La clave foránea está en PreciosEspecialidades
                .OnDelete(DeleteBehavior.NoAction); // Configuración del comportamiento de eliminación

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
                .HasMany(p => p.HistoriasClinicas)
                .WithOne(h => h.Pacientes)
                .HasForeignKey(h => h.PacientesId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Diagnosticos>()
                .HasOne(d => d.HistoriasClinicas)
                .WithMany(h => h.Diagnosticos)
                .HasForeignKey(d => d.HistoriasClinicasId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ResultadosEstudios>()
                .HasOne(d => d.HistoriasClinicas)
                .WithMany(h => h.ResultadosEstudios)
                .HasForeignKey(d => d.HistoriasClinicasId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recetas>()
                .HasOne(d => d.HistoriasClinicas)
                .WithMany(h => h.Recetas)
                .HasForeignKey(d => d.HistoriasClinicasId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recetas>()
                .HasMany(r => r.Medicamentos)
                .WithMany(m => m.Recetas)
                .UsingEntity(j => j.ToTable("RecetasMedicamentos"));

            modelBuilder.Entity<Medicos>()
                .HasMany(r => r.Especialidades)
                .WithMany(m => m.Medicos)
                .UsingEntity(j => j.ToTable("MedicosEspecialidades"));

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

            // Relación uno a uno entre Articulos y TiposSeguros
            modelBuilder.Entity<Articulos>()
                .HasOne(a => a.TiposSeguros) // Articulo tiene un TiposSeguros relacionado
                .WithOne(ts => ts.Articulos) // TiposSeguros tiene un Articulo relacionado
                .HasForeignKey<TiposSeguros>(ts => ts.ArticulosId) // Clave foránea en TiposSeguros
                .OnDelete(DeleteBehavior.NoAction); // Configuración del comportamiento de eliminación

            // Relación uno a muchos con TiposSeguros sin navegabilidad inversa
            modelBuilder.Entity<PreciosEspecialidades>()
                .HasOne(pe => pe.TiposSeguros)
                .WithMany() // Sin navegabilidad inversa
                .HasForeignKey(pe => pe.TiposSegurosId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relación uno a muchos con Especialidades sin navegabilidad inversa
            modelBuilder.Entity<PreciosEspecialidades>()
                .HasOne(pe => pe.Especialidades)
                .WithMany() // Sin navegabilidad inversa
                .HasForeignKey(pe => pe.EspecialidadesId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relación uno a uno entre Pacientes y Facturas
            modelBuilder.Entity<Pacientes>()
                .HasOne(p => p.Facturas)
                .WithOne(f => f.Pacientes)
                .HasForeignKey<Pacientes>(p => p.FacturasId) // La clave foránea está en Pacientes
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Facturas>()
                .HasMany(f => f.FacturasMes)
                .WithOne(fm => fm.Facturas)
                .HasForeignKey(fm => fm.FacturasId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FacturasMes>()
                .HasOne(fm => fm.ContratosSeguros)
                .WithMany()
                .HasForeignKey(fm => fm.ContratosSegurosId)
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
        public DbSet<FacturasMes> FacturasMes { get; set; }

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
