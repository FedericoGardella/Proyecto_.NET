using DAL.IDALs;
using DAL.Models;
using Microsoft.Extensions.Logging;
using Shared.DTOs;
using Shared.Entities;
using System.Net.Http;

namespace DAL.DALs
{
    public class DAL_HistoriasClinicas_Service : IDAL_HistoriasClinicas
    {
        private readonly HttpClient _httpClient;

        public DAL_HistoriasClinicas_Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HistoriaClinica Update(HistoriaClinica historiaClinica)
        {
            throw new NotImplementedException();
        }

        public HistoriaClinica Add(HistoriaClinica historiaClinica)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public HistoriaClinica Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<HistoriaClinica> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<DiagnosticoDTO> GetDiagnosticos(long historiaClinicaId)
        {
            throw new NotImplementedException();
        }

        public List<ResultadoEstudio> GetResultadoEstudios(long historiaClinicaId)
        {
            throw new NotImplementedException();
        }

        public List<Receta> GetRecetas(long historiaClinicaId)
        {
            throw new NotImplementedException();
        }

        public List<HistoriaClinicaDTO> GetHistoriasByDocumento(string documento)
        {
            throw new NotImplementedException();
        }

        public HistoriaClinica GetUltimaHistoriaClinicaPorPaciente(long pacienteId, string token)
        {
            try
            {

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"{token}");

                var url = $"http://host.docker.internal:8084/api/HistoriasClinicas/Pacientes/{pacienteId}/UltimaHistoriaClinica";

                var response = _httpClient.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al llamar al servicio externo: {response.ReasonPhrase}");
                }

                var content = response.Content.ReadAsStringAsync().Result;

                var ultimaHistoria = System.Text.Json.JsonSerializer.Deserialize<HistoriaClinica>(content, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return ultimaHistoria;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la última historia clínica desde el servicio externo.", ex);
            }
        }

        public bool Existe(long especialidadId, long tipoSeguroId)
        {
            throw new NotImplementedException();
        }

    }
}
