using Shared.DTOs;
using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_HistoriasClinicas
    {
        HistoriaClinica Get(long Id);
        List<HistoriaClinica> GetAll();
        HistoriaClinica Add(HistoriaClinica x);
        HistoriaClinica Update(HistoriaClinica x);
        void Delete(long Id);
        List<DiagnosticoDTO> GetDiagnosticos(long historiaClinicaId);
        List<ResultadoEstudio> GetResultadoEstudios(long historiaClinicaId);
        List<Receta> GetRecetas(long historiaClinicaId);
        List<HistoriaClinicaDTO> GetHistoriasXDocumento(string documento);
        HistoriaClinica GetUltimaHistoriaClinicaPorPaciente(long pacienteId, string token);
    }
}
