using System.Net.Http;
using System.Text.Json;
using DAL.IDALs;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_Especialidades_Service : IDAL_Especialidades
    {
        private readonly HttpClient _httpClient;

        public DAL_Especialidades_Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Especialidad Get(long Id)
        {
            throw new NotImplementedException();
        }

        public List<Especialidad> GetByIds(List<long> ids)
        {
            try
            {
                // Validar que la lista de IDs no sea nula o vacía
                if (ids == null || !ids.Any())
                {
                    throw new ArgumentException("La lista de IDs no puede ser nula o vacía.");
                }

                // Crear la URL con los IDs como parámetros de consulta
                var queryString = string.Join("&", ids.Select(id => $"ids={id}"));
                var url = $"http://host.docker.internal:8081/api/Especialidades/GetByIds?{queryString}";

                // Realizar la solicitud GET al endpoint
                var response = _httpClient.GetAsync(url).Result;

                // Verificar si la respuesta fue exitosa
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al llamar al servicio externo: {response.ReasonPhrase}");
                }

                // Leer el contenido de la respuesta como una lista de Especialidad
                var content = response.Content.ReadAsStringAsync().Result;
                var especialidades = JsonSerializer.Deserialize<List<Especialidad>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return especialidades ?? new List<Especialidad>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener especialidades desde el servicio externo.", ex);
            }
        }

        public List<Especialidad> GetAll()
        {
            throw new NotImplementedException();
        }

        public Especialidad Add(Especialidad x)
        {
            throw new NotImplementedException();
        }

        public Especialidad Update(Especialidad x)
        {
            throw new NotImplementedException();
        }

        public void Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public List<Especialidad> GetEspecialidadesPorMedico(long medicoId)
        {
            throw new NotImplementedException();
        }
    }
}
