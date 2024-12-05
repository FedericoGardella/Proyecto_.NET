using System.Collections.Generic;
using DAL.IDALs;
using DAL.Models;
using Shared.DTOs;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_Pacientes_Service : IDAL_Pacientes
    {

        private readonly HttpClient _httpClient;

        public DAL_Pacientes_Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Paciente Add(Paciente x)
        {
            throw new NotImplementedException();
        }

        public void Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public Paciente Get(long Id, string token)
        {
            try
            {

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"{token}");

                var url = $"http://host.docker.internal:8082/api/Pacientes/{Id}";

                var response = _httpClient.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al llamar al servicio externo: {response.ReasonPhrase}");
                }

                var content = response.Content.ReadAsStringAsync().Result;

                var pacienteObtenido = System.Text.Json.JsonSerializer.Deserialize<Paciente>(content, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return pacienteObtenido;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la última historia clínica desde el servicio externo.", ex);
            }
        }

        public List<Paciente> GetAll()
        {
            throw new NotImplementedException();
        }

        public Paciente GetPacienteByDocumento(string documento)
        {
            // Lista mock de pacientes
            var pacientesMock = new List<Paciente>
            {
                new Paciente
                {
                    Id = 60005,
                    Nombres = "Juan",
                    Apellidos = "Pérez",
                    Documento = "12345678",
                    Telefono = "099123456",
                    Citas = new List<Cita>(), // Dejar vacío para el mock
                    HistoriasClinicas = new List<HistoriaClinica>(), // Dejar vacío para el mock
                    ContratosSeguros = new List<ContratoSeguro>()
                },
                new Paciente
                {
                    Id = 2,
                    Nombres = "Ana",
                    Apellidos = "Gómez",
                    Documento = "87654321",
                    Telefono = "098765432",
                    Citas = new List<Cita>(),
                    HistoriasClinicas = new List<HistoriaClinica>(),
                    ContratosSeguros = new List<ContratoSeguro>()
                }
            };

            // Buscar el paciente en la lista mock
            var paciente = pacientesMock.FirstOrDefault(p => p.Documento == documento);

            if (paciente == null)
            {
                throw new Exception($"No se encontró un paciente con el documento {documento}.");
            }

            return paciente;
        }

        public Paciente Update(Paciente x)
        {
            throw new NotImplementedException();
        }

        public List<ContratoSeguroDTO> GetContratosSeguros(long PacienteId) 
        { 
            throw new NotImplementedException(); 
        }

        public string GetEmail(long pacienteId)
        {
            try
            {
                var url = $"http://host.docker.internal:8082/api/Pacientes/{pacienteId}/email";

                var response = _httpClient.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al llamar al servicio externo: {response.ReasonPhrase}");
                }

                // Si el contenido es un string plano
                var email = response.Content.ReadAsStringAsync().Result;

                if (string.IsNullOrEmpty(email))
                {
                    throw new Exception("El servicio externo no devolvió un email válido.");
                }

                return email.Trim();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el email desde el servicio externo.", ex);
            }
        }
    }
}
