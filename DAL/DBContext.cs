using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace DAL
{
    public class DBContext : DbContext
    {
        private string _connectionString = "Server=sqlserver,1433;Database=Practico3;User Id=sa;Password=Abc*123!;TrustServerCertificate=True";

        public DBContext() { }

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {           

            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pacientes>()
                .HasOne(p => p.historiaClinica)
                .WithOne(h => h.paciente)
                .HasForeignKey<HistoriasClinicas>(h => h.Id);
        }

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

        public static void UpdateDatabase()
        {
            using (var context = new DBContext())
            {
                context.Database.Migrate();
            }
        }
    }
}
