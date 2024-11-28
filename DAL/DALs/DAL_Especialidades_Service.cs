using DAL.IDALs;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_Especialidades_Service : IDAL_Especialidades
    {
        public Especialidad Add(Especialidad x)
        {
            throw new NotImplementedException();
        }

        public void Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public Especialidad Get(long Id)
        {
            throw new NotImplementedException();
        }

        public List<Especialidad> GetByIds(List<long> ids)
        {
            // Simulamos una lista de especialidades en memoria
            var mockEspecialidades = new List<Especialidad>
            {
                new Especialidad { Id = 1, Nombre = "Cardiología", tiempoCita = TimeSpan.FromMinutes(30) },
                new Especialidad { Id = 2, Nombre = "Dermatología", tiempoCita = TimeSpan.FromMinutes(20) },
                new Especialidad { Id = 3, Nombre = "Neurología", tiempoCita = TimeSpan.FromMinutes(40) },
                new Especialidad { Id = 4, Nombre = "Oftalmología", tiempoCita = TimeSpan.FromMinutes(25) }
            };

            // Si la lista de IDs es nula o está vacía, devolvemos una lista vacía
            if (ids == null || ids.Count == 0)
            {
                return new List<Especialidad>();
            }

            // Filtramos las especialidades que coinciden con los IDs proporcionados
            return mockEspecialidades
                   .Where(m => ids.Contains(m.Id))
                   .ToList();
        }


        public List<Especialidad> GetAll()
        {
            throw new NotImplementedException();
        }

        public Especialidad Update(Especialidad x)
        {
            throw new NotImplementedException();
        }
    }

}
