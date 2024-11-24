using DAL.Models;
using Shared.DTOs;
using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_HistoriasClinicas
    {
        HistoriaClinica Get(long Id);
        List<HistoriaClinica> GetAll();
        HistoriaClinica Add(HistoriaClinica x);
        HistoriaClinica Update(HistoriaClinica x);
        void Delete(long Id);
        List<DiagnosticoDTO> GetDiagnosticos(long historiaClinicaId);
        List<ResultadoEstudio> GetResultadoEstudios(long historiaClinicaId);
        List<Receta> GetRecetas(long historiaClinicaId);
        public List<HistoriaClinicaDTO> GetHistoriasByDocumento(string documento);
    }
}
