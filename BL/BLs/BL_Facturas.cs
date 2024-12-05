using System.Net.Http;
using BL.IBLs;
using DAL.IDALs;
using DAL.Models;
using Shared.DTOs;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_Facturas : IBL_Facturas
    {
        private IDAL_Facturas dal;
        private IBL_Pacientes blPacientes;

        public BL_Facturas(IDAL_Facturas _dal, IBL_Pacientes _blPacientes)
        {
            dal = _dal;
            blPacientes = _blPacientes;
        }

        public Factura Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<Factura> GetAll()
        {
            return dal.GetAll();
        }

        public Factura Add(Factura x)
        {
            return dal.Add(x);
        }

        public Factura Update(Factura x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }

        public List<FacturaDTO> GetFacturasPorPaciente(long pacienteId)
        {
            var facturas = dal.GetFacturasPorPaciente(pacienteId);
            return facturas.Select(f => new FacturaDTO
            {
                Id = f.Id,
                Fecha = f.Fecha,
                Pago = f.Pago,
                Costo = f.Costo,
                ContratoSeguroId = f.ContratoSeguroId,
                CitaId = f.CitaId
            }).ToList();
        }

        public void GenerarFacturasMensuales(string token)
        {

            var pacientes = blPacientes.GetAllPacientes(token);

            foreach (var paciente in pacientes)
            {
                // Calcular el rango de fechas para el mes anterior
                var inicioMesAnterior = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(-1);
                var finMesAnterior = inicioMesAnterior.AddMonths(1).AddSeconds(-1);

                // Obtener todas las facturas del mes anterior que no sean mensuales
                var facturasDelMes = dal.GetFacturasPorPaciente(paciente.Id)
                    .Where(f => f.Fecha >= inicioMesAnterior && f.Fecha <= finMesAnterior)
                    .ToList();

                if (!facturasDelMes.Any())
                {
                    // Si no hay facturas del mes anterior, nada que generar
                    continue;
                }

                // Calcular el costo total de todas las facturas del mes
                decimal costoTotal = facturasDelMes.Sum(f => f.Costo);

                // Crear una nueva factura mensual consolidada
                var facturaMensual = new Factura
                {
                    Fecha = DateTime.UtcNow, // Fecha actual para la nueva factura
                    Pago = false,
                    Costo = costoTotal,
                    PacienteId = paciente.Id,
                    Mensual = true // Marcar como factura mensual
                };

                // Guardar la nueva factura mensual
                Add(facturaMensual);
            }
        }

        public List<FacturaDTO> GetFacturasMensuales(long pacienteId)
        {
            var facturas = dal.GetFacturasMensuales(pacienteId);
            return facturas.Select(f => new FacturaDTO
            {
                Id = f.Id,
                Fecha = f.Fecha,
                Pago = f.Pago,
                Costo = f.Costo,
                Mensual = f.Mensual,
                PacienteId = f.PacienteId
            }).ToList();
        }


    }
}
