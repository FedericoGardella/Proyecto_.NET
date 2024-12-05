using BL.IBLs;
using DAL.IDALs;
using DAL.Models;
using Shared.DTOs;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_GruposCitas : IBL_GruposCitas
    {
        private IDAL_GruposCitas dal;

        public BL_GruposCitas(IDAL_GruposCitas _dal)
        {
            dal = _dal;
        }

        public GrupoCita Get(long Id)
        {
            return dal.Get(Id);
        }


        public List<GrupoCita> GetAll()
        {
            return dal.GetAll();
        }

        public GrupoCita GetGrupoCitasMedico(long medicoDoc, DateTime fecha, string token)
        {

            var grupoCitas = dal.GetGrupoCitasMedico(medicoDoc, fecha, token);

            return grupoCitas;
        }

        public GrupoCita Add(GrupoCita x)
        {
            return dal.Add(x);
        }

        public GrupoCita AddGrupoCitaConCitas(GrupoCitaPostDTO dto)
        {
            return dal.AddGrupoCitaConCitas(dto);
        }

        public GrupoCita GetDetalle(long id)
        {
            var grupoCita = dal.GetDetalle(id);

            if (grupoCita == null)
            {
                return null;
            }

            return grupoCita.GetEntity();
        }

        public GrupoCita Update(GrupoCita x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }

        public List<GrupoCitaDTO> GetByEspecialidadAndMes(long especialidadId, int mes)
        {
            var gruposCitas = dal.GetByEspecialidadAndMes(especialidadId, mes);
            return gruposCitas.Select(gc => new GrupoCitaDTO
            {
                Id = gc.Id,
                Lugar = gc.Lugar,
                Fecha = gc.Fecha
            }).ToList();
        }

        public void UpdatePaciente(long citaId, long pacienteId)
        {
            throw new NotImplementedException();
        }
    }
}
