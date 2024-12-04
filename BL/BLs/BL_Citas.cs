using System.Net.Http;
using BL.IBLs;
using DAL.IDALs;
using DAL.Models;
using Microsoft.Extensions.Logging;
using Shared.DTOs;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_Citas : IBL_Citas
    {
        private IDAL_Citas dal;
        private IBL_GruposCitas blGruposCitas;
        private IBL_PreciosEspecialidades blPreciosEspecialidades;
        private IBL_Facturas blFacturas;
        private IBL_Pacientes blPacientes;

        public BL_Citas(IDAL_Citas _dal, IBL_GruposCitas _bGruposCitas, IBL_PreciosEspecialidades _blPreciosEspecialidades, IBL_Facturas _blFacturas, IBL_Pacientes _blPacientes)
        {
            dal = _dal;
            blGruposCitas = _bGruposCitas;
            blPreciosEspecialidades = _blPreciosEspecialidades;
            blFacturas = _blFacturas;
            blPacientes = _blPacientes;
        }

        public Cita Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<Cita> GetAll()
        {
            return dal.GetAll();
        }

        public Cita Add(Cita x)
        {
            return dal.Add(x);
        }

        public Cita Update(Cita x)
        {
            return dal.Update(x);
        }

        public void UpdatePaciente(long citaId, long pacienteId, long tipoSeguroId)
        {
            var cita = dal.Get(citaId);

            if (cita == null)
            {
                throw new Exception($"No se encontró la cita con ID {citaId}");
            }

            if (cita.PacienteId != null)
            {
                throw new Exception("La cita ya está asignada a otro paciente.");
            }

            // Obtener el GrupoCita relacionado
            var grupoCita = blGruposCitas.Get(cita.GrupoCitaId);
            if (grupoCita == null)
            {
                throw new Exception($"No se encontró el GrupoCita con ID {cita.GrupoCitaId}");
            }

            var especialidadId = grupoCita.EspecialidadId;

            var costo = blPreciosEspecialidades.GetCosto(especialidadId, tipoSeguroId);
            if (costo <= 0)
            {
                throw new Exception($"No se pudo obtener el costo para EspecialidadId {especialidadId} y TipoSeguroId {tipoSeguroId}.");
            }

            cita.PacienteId = pacienteId;
            dal.UpdatePaciente(cita);

            var nuevaFactura = new Factura
            {
                PacienteId = pacienteId,
                CitaId = cita.Id,
                Fecha = DateTime.UtcNow,
                Costo = costo,
                Pago = false // Por defecto, no está pagada
            };

            var facturaCreada = blFacturas.Add(nuevaFactura);
            if (facturaCreada == null)
            {
                throw new Exception("No se pudo crear la factura asociada a la cita.");
            }
        }


        public void Delete(long Id)
        {
            dal.Delete(Id);
        }
    }
}
