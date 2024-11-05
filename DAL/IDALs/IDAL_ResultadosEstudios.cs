using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_ResultadosEstudios
    {
        ResultadoEstudio Get(long Id);
        List<ResultadoEstudio> GetAll();
        ResultadoEstudio Add(ResultadoEstudio x);
        ResultadoEstudio Update(ResultadoEstudio x);
        void Delete(long Id);
    }
}
