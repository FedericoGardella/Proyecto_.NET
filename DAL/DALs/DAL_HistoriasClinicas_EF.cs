using DAL.IDALs;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DALs
{
    public class DAL_HistoriasClinicas_EF : IDAL_HistoriasClinicas
    {
        private DBContext db;
        private string entityName = "HistoriaClinica";

        public DAL_HistoriasClinicas_EF(DBContext _db)
        {
            db = _db;
        }

        public HistoriaClinica Get(long Id)
        {
            return db.HistoriasClinicas.Find(Id)?.GetEntity();
        }

        public List<HistoriaClinica> GetAll()
        {
            return db.HistoriasClinicas.Select(x => x.GetEntity()).ToList();
        }

        public HistoriaClinica Add(HistoriaClinica x)
        {
            HistoriasClinicas toSave = new HistoriasClinicas();
            toSave = HistoriasClinicas.FromEntity(x, toSave);
            db.HistoriasClinicas.Add(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public HistoriaClinica Update(HistoriaClinica x)
        {
            HistoriasClinicas toSave = db.HistoriasClinicas.FirstOrDefault(c => c.Id == x.Id);
            toSave = HistoriasClinicas.FromEntity(x, toSave);
            db.Update(toSave);
            db.SaveChanges();
            return Get(toSave.Id);
        }

        public void Delete(long Id)
        {
            HistoriasClinicas? toDelete = db.HistoriasClinicas.Find(Id);
            if (toDelete == null)
                throw new Exception($"No existe un {entityName} con Id {Id}");
            db.HistoriasClinicas.Remove(toDelete);
            db.SaveChanges();
        }

        public List<DiagnosticoDTO> GetDiagnosticos(long historiaClinicaId)
        {
            var diagnosticos = db.HistoriasClinicas
                .Where(h => h.Id == historiaClinicaId)
                .SelectMany(h => h.Diagnosticos)
                .ToList();

            return diagnosticos.Select(d => new DiagnosticoDTO
            {
                Id = d.Id,
                Descripcion = d.Descripcion,
                Fecha = d.Fecha,
                HistoriaClinicaId = d.HistoriasClinicasId
            }).ToList();
        }

        public List<ResultadoEstudio> GetResultadoEstudios(long historiaClinicaId)
        {
            var diagnosticos = db.HistoriasClinicas
                .Where(h => h.Id == historiaClinicaId)
                .SelectMany(h => h.ResultadosEstudios)
                .ToList();

            return diagnosticos.Select(d => new ResultadoEstudio
            {
                Id = d.Id,
                Descripcion = d.Descripcion,
                Fecha = d.Fecha,
            }).ToList();
        }

        public List<Receta> GetRecetas(long historiaClinicaId)
        {
            var receta = db.HistoriasClinicas
                .Where(h => h.Id == historiaClinicaId)
                .SelectMany(h => h.Recetas)
                .ToList();

            return receta.Select(d => new Receta
            {
                Id = d.Id,
                Descripcion = d.Descripcion,
                Fecha = d.Fecha,
                Tipo = d.Tipo,
            }).ToList();
        }
        public List<HistoriaClinicaDTO> GetHistoriasByDocumento(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
            {
                throw new ArgumentException("El documento no puede estar vacío.", nameof(documento));
            }

            // Obtener el paciente por documento y sus historias clínicas
            var paciente = db.Pacientes
                .Include(p => p.HistoriasClinicas) // Incluir las historias clínicas en la consulta
                .FirstOrDefault(p => p.Documento == documento);

            if (paciente == null || paciente.HistoriasClinicas == null || !paciente.HistoriasClinicas.Any())
            {
                return new List<HistoriaClinicaDTO>(); // Devuelve una lista vacía si no hay resultados
            }

            // Mapear HistoriasClinicas a HistoriaClinicaDTO
            return paciente.HistoriasClinicas.Select(h => new HistoriaClinicaDTO
            {
                Id = h.Id,
                PacienteId = paciente.Id,
                PacienteNombres = paciente.Nombres,
                PacienteApellidos = paciente.Apellidos,
                FechaCreacion = h.FechaCreacion,
                Comentarios = h.Comentarios,
                NombreMedico = h.NombreMedico,
                CitaId = h.CitasId
            }).ToList();
        }
        public HistoriaClinica GetUltimaHistoriaClinicaPorPaciente(long pacienteId, string token)
        {
            var ultimaHistoria = db.HistoriasClinicas
                .Include(h => h.ResultadosEstudios)
                .Include(h => h.Diagnosticos)
                .Include(h => h.Recetas)
                    .ThenInclude(r => r.Medicamentos)
                .Where(h => h.PacientesId == pacienteId)
                .OrderByDescending(h => h.FechaCreacion)
                .FirstOrDefault();

            return ultimaHistoria?.GetEntity();
        }
    }
}
