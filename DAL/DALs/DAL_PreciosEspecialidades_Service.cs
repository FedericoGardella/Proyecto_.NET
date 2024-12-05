using DAL.IDALs;
using Shared.Entities;
using System.Text.Json;

namespace DAL.DALs
{
    public class DAL_PreciosEspecialidades_Service : IDAL_PreciosEspecialidades
    {
        private readonly HttpClient _httpClient;

        public DAL_PreciosEspecialidades_Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public PrecioEspecialidad Update(PrecioEspecialidad precioEspecialidad)
        {
            throw new NotImplementedException();
        }

        public PrecioEspecialidad Add(PrecioEspecialidad precioEspecialidad)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public PrecioEspecialidad Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<PrecioEspecialidad> GetAll()
        {
            throw new NotImplementedException();
        }

        public PrecioEspecialidad GetByEspecialidadAndTipoSeguro(long especialidadId, long tipoSeguroId) 
        { 
            throw new NotImplementedException(); 
        }

        public bool Repetido(long especialidadId, long tipoSeguroId) 
        {  
            throw new NotImplementedException(); 
        }

        public decimal GetCosto(long especialidadId, long tipoSeguroId)
        {
            try
            {
                // Construir la URL del endpoint externo
                var url = $"http://host.docker.internal:8081/api/PreciosEspecialidades/GetCosto?especialidadId={especialidadId}&tipoSeguroId={tipoSeguroId}";

                // Realizar la solicitud GET al endpoint
                var response = _httpClient.GetAsync(url).Result;

                // Verificar si la respuesta fue exitosa
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al llamar al servicio externo: {response.ReasonPhrase}");
                }

                // Leer el contenido de la respuesta como decimal
                var content = response.Content.ReadAsStringAsync().Result;
                var costo = JsonSerializer.Deserialize<decimal>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return costo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el costo desde el servicio externo.", ex);
            }
        }
    }
}
