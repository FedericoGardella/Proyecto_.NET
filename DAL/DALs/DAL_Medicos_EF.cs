using DAL.IDALs;
using DAL.Models;
using Shared.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;

namespace DAL.DALs
{
    public class DAL_Medicos_EF : IDAL_Medicos
    {
        private readonly DBContext db;
        private readonly ILogger<DAL_Medicos_EF> logger;
        private readonly string entityName = "Medico";

        public DAL_Medicos_EF(DBContext _db, ILogger<DAL_Medicos_EF> _logger)
        {
            db = _db;
            logger = _logger;
        }

        public Medico Get(long Id)
        {
            return db.Medicos.Find(Id)?.GetEntity();
        }

        public List<Medico> GetAll()
        {
            return db.Medicos.Select(x => x.GetEntity()).ToList();
        }

        public Medico Add(Medico x)
        {
            Medicos toSave = Medicos.FromEntity(x, new Medicos());

            var trackedEntity = db.Medicos.Local.FirstOrDefault(p => p.Id == toSave.Id);

            if (trackedEntity != null)
            {
                db.Entry(trackedEntity).CurrentValues.SetValues(toSave);
            }
            else
            {
                db.Medicos.Add(toSave);
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al guardar el médico y sus especialidades.", ex);
            }

            // Verificar si hay especialidades asociadas
            if (x.Especialidades != null && x.Especialidades.Count > 0)
            {
                // Crear las relaciones en la tabla intermedia manualmente para no buscar las especialidades en el db
                // ya que otro servicio es responsable de ellas
                var especialidadesMedicos = x.Especialidades.Select(especialidad => new
                {
                    EspecialidadId = especialidad.Id,
                    MedicoId = toSave.Id
                }).ToList();

                // Insertar directamente en la tabla intermedia
                foreach (var rel in especialidadesMedicos)
                {
                    db.Database.ExecuteSqlRaw(
                        "INSERT INTO MedicosEspecialidades (EspecialidadesId, MedicosId) VALUES ({0}, {1})",
                        rel.EspecialidadId,
                        rel.MedicoId
                    );
                }
            }

            return Get(toSave.Id);
        }

        public MedicoDTO Update(MedicoDTO medicoDto)
        {
            // Validar que el DTO no sea nulo
            if (medicoDto == null) throw new ArgumentNullException(nameof(medicoDto));

            // Buscar el médico en la base de datos
            var toSave = db.Medicos.FirstOrDefault(c => c.Id == medicoDto.Id);

            // Validar que el médico exista
            if (toSave == null)
            {
                throw new InvalidOperationException($"No se encontró el médico con ID {medicoDto.Id}");
            }

            // Actualizar los campos simples del médico
            toSave.Nombres = medicoDto.Nombres;
            toSave.Apellidos = medicoDto.Apellidos;
            toSave.Documento = medicoDto.Documento;
            toSave.Matricula = medicoDto.Matricula;

            // Asegurarse de que la lista de especialidades no sea tocada
            toSave.Especialidades = toSave.Especialidades ?? new List<Especialidades>();

            // Guardar cambios en la base de datos
            db.Medicos.Update(toSave);
            db.SaveChanges();

            // Devolver el médico actualizado como DTO
            return new MedicoDTO
            {
                Id = toSave.Id,
                Nombres = toSave.Nombres,
                Apellidos = toSave.Apellidos,
                Documento = toSave.Documento,
                Matricula = toSave.Matricula
            };
        }

        public void Delete(long Id)
        {
            Medicos? toDelete = db.Medicos.Find(Id);
                if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
                db.Medicos.Remove(toDelete);
                db.SaveChanges();
        }
    }
}
