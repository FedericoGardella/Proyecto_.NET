using BL.IBLs;
using DAL.IDALs;
using Shared.DTOs;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_HistoriasClinicas : IBL_HistoriasClinicas
    {
        private IDAL_HistoriasClinicas dal;

        public BL_HistoriasClinicas(IDAL_HistoriasClinicas _dal)
        {
            dal = _dal;
        }

        public HistoriaClinica Get(long Id)
        {
            return dal.Get(Id);
        }

        public List<HistoriaClinica> GetAll()
        {
            return dal.GetAll();
        }

        public HistoriaClinica Add(HistoriaClinica x)
        {
            return dal.Add(x);
        }

        public HistoriaClinica Update(HistoriaClinica x)
        {
            return dal.Update(x);
        }

        public void Delete(long Id)
        {
            dal.Delete(Id);
        }

        public List<DiagnosticoDTO> GetDiagnosticos(long historiaClinicaId)
        {
            return dal.GetDiagnosticos(historiaClinicaId);
        }

        public List<ResultadoEstudio> GetResultadoEstudios(long historiaClinicaId)
        {
            return dal.GetResultadoEstudios(historiaClinicaId);
        }
    }
}
