using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_ResultadosEstudios
    {
        ResultadoEstudio Get(long Id);
        List<ResultadoEstudio> GetAll();
        ResultadoEstudio Add(ResultadoEstudio x);
        ResultadoEstudio Update(ResultadoEstudio x);
        void Delete(long Id);
    }
}
