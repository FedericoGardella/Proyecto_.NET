using DAL.IDALs;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_Pacientes_EF : IDAL_Pacientes
    {
        private DBContext db;
        private string entityName = "Paciente";

        public DAL_Pacientes_EF(DBContext _db)
        {
            db = _db;
        }

        public Paciente Get(long Id, string token)
        {
            var paciente = db.Personas
                .OfType<Pacientes>()
                .Include(p => p.ContratosSeguros)
                .SingleOrDefault(p => p.Id == Id);

            if (paciente == null)
            {
                throw new Exception($"Paciente con ID {Id} no encontrado en la base de datos.");
            }

            return paciente.GetEntity();
        }


        public List<Paciente> GetAll()
        {
            return db.Pacientes.Select(x => x.GetEntity()).ToList();
        }

        public Paciente Add(Paciente x)
        {
            // Convertimos `Paciente` a `Pacientes` para almacenarlo en la base de datos.
            Pacientes toSave = Pacientes.FromEntity(x, new Pacientes());

            // Verificamos si el `dbContext` ya está rastreando esta entidad.
            var trackedEntity = db.Pacientes.Local.FirstOrDefault(p => p.Id == toSave.Id);

            if (trackedEntity != null)
            {
                // Si ya existe una instancia en el contexto, actualizamos los valores
                db.Entry(trackedEntity).CurrentValues.SetValues(toSave);
            }
            else
            {
                // Si es una entidad nueva, la añadimos al contexto
                db.Pacientes.Add(toSave);
            }

            db.SaveChanges();

            // Retornamos la entidad recién agregada, utilizando el método `Get` para obtener su versión más actualizada.
            return Get(toSave.Id, null);
        }

        public PacienteDTO Update(PacienteDTO pacienteDto)
        {
            // Verificar si el DTO es válido
            if (pacienteDto == null || pacienteDto.Id <= 0)
            {
                throw new ArgumentException("El paciente DTO proporcionado no es válido.");
            }

            // Buscar el paciente existente en la base de datos
            Pacientes pacienteExistente = db.Pacientes.FirstOrDefault(c => c.Id == pacienteDto.Id);

            if (pacienteExistente == null)
            {
                throw new InvalidOperationException($"No se encontró un paciente con el ID {pacienteDto.Id}.");
            }

            // Mapear los datos del DTO al modelo existente
            pacienteExistente = Pacientes.FromEntity(new Paciente
            {
                Id = pacienteDto.Id,
                Nombres = pacienteDto.Nombres,
                Apellidos = pacienteDto.Apellidos,
                Documento = pacienteDto.Documento,
                Telefono = pacienteDto.Telefono
            }, pacienteExistente);

            // Actualizar la base de datos
            db.Pacientes.Update(pacienteExistente);
            db.SaveChanges();

            // Retornar el DTO actualizado
            return new PacienteDTO
            {
                Id = pacienteExistente.Id,
                Nombres = pacienteExistente.Nombres,
                Apellidos = pacienteExistente.Apellidos,
                Documento = pacienteExistente.Documento,
                Telefono = pacienteExistente.Telefono,
                HistoriaClinicaId = 0
            };
        }


        public void Delete(long Id)
        {
            Pacientes? toDelete = db.Pacientes.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.Pacientes.Remove(toDelete);
            db.SaveChanges();
        }

        public Paciente GetPacienteByDocumento(string documento)
        {
            var paciente = db.Pacientes.AsNoTracking().FirstOrDefault(p => p.Documento == documento);
            return paciente?.GetEntity();
        }
        public List<ContratoSeguroDTO> GetContratosSeguros(long PacienteId)
        {
            var contratosSeguros = db.Pacientes
                .Where(p =>  p.Id == PacienteId)
                .SelectMany(p  => p.ContratosSeguros)
                .ToList();

            return contratosSeguros.Select(c  => new ContratoSeguroDTO
            {
                Id = c.Id,
                Activo = c.Activo,
                FechaInicio = c.FechaInicio,
                PacienteId = c.PacientesId,
                TipoSeguroId = c.TiposSegurosId
            }).ToList();
        }
        public string GetEmail(long pacienteId)
        {
            // Busca el usuario relacionado con el paciente
            var usuario = db.Users
                .FirstOrDefault(u => u.Personas.Id == pacienteId);

            // Devuelve el email del usuario si existe, de lo contrario null
            return usuario?.Username;
        }
    }
}
