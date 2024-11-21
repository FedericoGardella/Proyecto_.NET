using BL.IBLs;
using DAL.IDALs;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_Citas : IBL_Citas
    {
        private IDAL_Citas dal;

        public BL_Citas(IDAL_Citas _dal)
        {
            dal = _dal;
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

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }

        public List<Cita> GetCitasPorPacienteID(long PacienteID)
        {
           return dal.GetCitasPorPacienteId(PacienteID);
        }
    }
}
