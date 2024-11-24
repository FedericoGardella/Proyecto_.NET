using DAL.IDALs;
using DAL.Models;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_Pacientes_Service : IDAL_Pacientes
    {
        public Paciente Add(Paciente x)
        {
            throw new NotImplementedException();
        }

        public void Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public Paciente Get(long Id)
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
                    ContratoSeguroId = 1,
                    ContratoSeguro = null // Mock sin detalle
                },
                new Paciente
                {
                    Id = 12522,
                    Nombres = "Ana",
                    Apellidos = "Gómez",
                    Documento = "87654321",
                    Telefono = "098765432",
                    Citas = new List<Cita>(),
                    HistoriasClinicas = new List<HistoriaClinica>(),
                    ContratoSeguroId = null,
                    ContratoSeguro = null
                }
            };

            // Buscar el paciente en la lista mock
            var paciente = pacientesMock.FirstOrDefault(p => p.Id == Id);

            if (paciente == null)
            {
                throw new Exception($"No se encontró un paciente con el id {Id}.");
            }

            return paciente;
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
                    ContratoSeguroId = 1,
                    ContratoSeguro = null // Mock sin detalle
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
                    ContratoSeguroId = null,
                    ContratoSeguro = null
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
    }
}
