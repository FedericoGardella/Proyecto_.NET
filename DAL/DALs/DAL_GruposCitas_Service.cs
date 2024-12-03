using DAL.IDALs;
using DAL.Models;
using Shared.DTOs;
using Shared.Entities;
using System.Net.Http;

namespace DAL.DALs
{
    public class DAL_GruposCitas_Service : IDAL_GruposCitas
    {
        private readonly HttpClient _httpClient;

        public DAL_GruposCitas_Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
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
        public GrupoCita GetGrupoCitasMedico(long medicoId, DateTime fecha, string token)
        {
            try
            {

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"{token}");

                var url = $"http://host.docker.internal:8083/api/GruposCitas/medico/{medicoId}/hoy";

                var response = _httpClient.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al llamar al servicio externo: {response.ReasonPhrase}");
                }

                var content = response.Content.ReadAsStringAsync().Result;

                var grupoObtenido = System.Text.Json.JsonSerializer.Deserialize<GrupoCita>(content, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return grupoObtenido;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el grupocita desde el servicio externo.", ex);
            }
        }

        public GrupoCita AddGrupoCitaConCitas(GrupoCitaPostDTO dto)
        {
            throw new NotImplementedException();
        }

        public List<GruposCitas> GetByEspecialidadAndMes(long especialidadId, int mes)
        {
            throw new NotImplementedException();
        }

        public GruposCitas GetDetalle(long id)
        {
            throw new NotImplementedException();
        }
    }
}
