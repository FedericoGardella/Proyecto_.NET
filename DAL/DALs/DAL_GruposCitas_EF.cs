using DAL.IDALs;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_GruposCitas_EF : IDAL_GruposCitas
    {
        private DBContext db;
        private string entityName = "GrupoCita";

        public DAL_GruposCitas_EF(DBContext _db)
        {
            db = _db;
        }

        public GrupoCita Get(long Id)
        {
            return db.GruposCitas.Find(Id)?.GetEntity();
        }

        public List<GrupoCita> GetAll()
        {
            return db.GruposCitas.Select(x => x.GetEntity()).ToList();
        }

        public GrupoCita GetGrupoCitasMedico(long medicoId, DateTime fecha)
        {
            throw new NotImplementedException();
        }

        public GrupoCita Add(GrupoCita x)
        {
            var grupoCitasToSave = GruposCitas.FromEntity(x, null);

            // Asociar cada cita al grupo
            if (x.Citas != null && x.Citas.Count > 0)
            {
                grupoCitasToSave.Citas = x.Citas.Select(c => Citas.FromEntity(c, null)).ToList();
            }

            var savedGrupoCitas = db.GruposCitas.Add(grupoCitasToSave);
            db.SaveChanges();

            return savedGrupoCitas.Entity.GetEntity();
        }

        public GrupoCita AddGrupoCitaConCitas(GrupoCitaPostDTO dto)
        {
            // Crear la estrategia de ejecución
            var strategy = db.Database.CreateExecutionStrategy();

            // Ejecutar las operaciones dentro de la estrategia
            return strategy.Execute(() =>
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // Crear la entidad GrupoCita
                        var grupoCita = new GruposCitas
                        {
                            Lugar = dto.Lugar,
                            Fecha = dto.Fecha,
                            MedicosId = dto.MedicoId,
                            EspecialidadesId = dto.EspecialidadId,
                            Citas = null
                        };

                        // Agregar el grupo de citas a la base de datos
                        db.GruposCitas.Add(grupoCita);
                        db.SaveChanges(); // Necesario para generar el Id del grupo

                        // Generar y guardar las citas
                        for (int i = 0; i < dto.CantidadCitas; i++)
                        {
                            var hora = dto.HoraInicio.Add(TimeSpan.FromMinutes(i * dto.IntervaloMinutos));

                            var cita = new Citas
                            {
                                Hora = hora,
                                Costo = 0, // Costo nulo
                                PacienteId = null, // Paciente nulo
                                PreciosEspecialidadesId = null, // PrecioEspecialidad nulo
                                GruposCitasId = grupoCita.Id // Asociar al grupo recién creado
                            };
                            db.Citas.Add(cita);
                        }

                        db.SaveChanges(); // Guardar todas las citas
                        transaction.Commit(); // Confirmar la transacción

                        return grupoCita.GetEntity();
                    }
                    catch
                    {
                        transaction.Rollback(); // Revertir los cambios si algo falla
                        throw; // Re-lanzar la excepción
                    }
                }
            });
        }


        public GrupoCita Update(GrupoCita x)
        {
            GruposCitas toSave = db.GruposCitas.FirstOrDefault(c => c.Id == x.Id);
            toSave = GruposCitas.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public List<GruposCitas> GetByEspecialidadAndMes(long especialidadId, int mes)
        {
            return db.GruposCitas
                .Where(gc => gc.EspecialidadesId == especialidadId && gc.Fecha.Month == mes)
                .ToList();
        }

        public void Delete(long Id)
        {
            GruposCitas? toDelete = db.GruposCitas.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.GruposCitas.Remove(toDelete);
            db.SaveChanges();
        }
    }
}
