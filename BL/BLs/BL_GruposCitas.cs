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

        public GrupoCita GetGrupoCitasMedico(long medicoDoc, DateTime fecha)
        {

            var grupoCitas = dal.GetGrupoCitasMedico(medicoDoc, fecha);

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

        public GrupoCita Update(GrupoCita x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }
    }
}
