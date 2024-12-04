using System.Net.Http;
using DAL.IDALs;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_Especialidades_Service : IDAL_Especialidades
    {
        private readonly List<Especialidad> _especialidades;

        public DAL_Especialidades_Service()
        {
            // Inicializar una lista fija de especialidades
            _especialidades = new List<Especialidad>
            {
                new Especialidad { Id = 1, Nombre = "Cardiología", tiempoCita = TimeSpan.FromMinutes(30) },
                new Especialidad { Id = 2, Nombre = "Dermatología", tiempoCita = TimeSpan.FromMinutes(20) },
                new Especialidad { Id = 3, Nombre = "Pediatría", tiempoCita = TimeSpan.FromMinutes(40) },
                new Especialidad { Id = 4, Nombre = "Oftalmología", tiempoCita = TimeSpan.FromMinutes(25) }
            };

        }

        // Obtener una especialidad por ID
        public Especialidad Get(long Id)
        {
            var especialidad = _especialidades.FirstOrDefault(e => e.Id == Id);
            if (especialidad == null)
            {
                throw new Exception($"Especialidad con ID {Id} no encontrada.");
            }
            return especialidad;
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
            return _especialidades;
        }

        // Agregar una nueva especialidad
        public Especialidad Add(Especialidad x)
        {
            x.Id = _especialidades.Any() ? _especialidades.Max(e => e.Id) + 1 : 1; // Generar un nuevo ID
            _especialidades.Add(x);
            return x;
        }

        // Actualizar una especialidad existente
        public Especialidad Update(Especialidad x)
        {
            var especialidad = _especialidades.FirstOrDefault(e => e.Id == x.Id);
            if (especialidad != null)
            {
                especialidad.Nombre = x.Nombre;
                especialidad.tiempoCita = x.tiempoCita;
            }
            return especialidad;
        }

        // Eliminar una especialidad por ID
        public void Delete(long Id)
        {
            var especialidad = _especialidades.FirstOrDefault(e => e.Id == Id);
            if (especialidad != null)
            {
                _especialidades.Remove(especialidad);
            }
        }


        //public Especialidad GetEspecialidad(long Id, string token)
        //{
        //    try
        //    {
        //        _httpClient.DefaultRequestHeaders.Clear();
        //        _httpClient.DefaultRequestHeaders.Add("Authorization", $"{token}");

        //        var url = $"___________________________{Id}";

        //        var response = _httpClient.GetAsync(url).Result;

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            throw new Exception($"Error al llamar al servicio externo: {response.ReasonPhrase}");
        //        }

        //        var content = response.Content.ReadAsStringAsync().Result;

        //        var especialidadObtenida = System.Text.Json.JsonSerializer.Deserialize<Especialidad>(content, new System.Text.Json.JsonSerializerOptions
        //        {
        //            PropertyNameCaseInsensitive = true
        //        });

        //        return especialidadObtenida;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al obtener la última historia clínica desde el servicio externo.", ex);
        //    }

        //}
    }

}
