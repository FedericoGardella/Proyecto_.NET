using DAL.IDALs;
using DAL.Models;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_GruposCitas_Service : IDAL_GruposCitas
    {
        public GrupoCita Update(GrupoCita grupoCita)
        {
            throw new NotImplementedException();
        }

        public GrupoCita Add(GrupoCita grupoCita)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public GrupoCita Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<GrupoCita> GetAll()
        {
            throw new NotImplementedException();
        }
        public GrupoCita GetGrupoCitasMedico(long medicoId, DateTime fecha)
        {
            // Datos hardcodeados de GruposCitas y sus relaciones
            var gruposCitasMock = new List<GruposCitas>
            {
                new GruposCitas
                {
                    Id = 1,
                    Lugar = "Clínica Central",
                    Fecha = new DateTime(2024, 11, 28),
                    MedicosId = 3,
                    EspecialidadesId = 201,
                    Citas = new List<Citas>
                    {
                        new Citas { Id = 5432, PacienteId = 2, Hora = new DateTime(2024, 11, 28, 9, 0, 0).TimeOfDay  },
                        new Citas { Id = 9864, PacienteId = 12522, Hora = new DateTime(2024, 11, 24, 10, 0, 0).TimeOfDay  }
                    }
                },
                new GruposCitas
                {
                    Id = 2,
                    Lugar = "Hospital Norte",
                    Fecha = new DateTime(2024, 11, 23),
                    MedicosId = 102,
                    EspecialidadesId = 202,
                    Citas = new List<Citas>
                    {
                        new Citas { Id = 3, PacienteId = 3, Hora = new DateTime(2024, 11, 23, 11, 0, 0).TimeOfDay  }
                    }
                }
            };

            // Filtrar por MedicoId y Fecha
            var grupo = gruposCitasMock
                .FirstOrDefault(g => g.MedicosId == medicoId && g.Fecha.Date == fecha.Date);

            if (grupo == null)
            {
                return null; // Si no se encuentra, retorna null
            }

            // Convertir GruposCitas a GrupoCita
            var grupoCita = new GrupoCita
            {
                Id = grupo.Id,
                Lugar = grupo.Lugar,
                Fecha = grupo.Fecha,
                MedicoId = grupo.MedicosId,
                EspecialidadId = grupo.EspecialidadesId,
                Citas = grupo.Citas.Select(c => new Cita
                {
                    Id = c.Id,
                    PacienteId = c.PacienteId,
                    Hora = c.Hora
                }).ToList()
            };

            return grupoCita;
        }
    }
}
