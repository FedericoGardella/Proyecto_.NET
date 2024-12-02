using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_PreciosEspecialidades
    {
        PrecioEspecialidad Get(long Id);
        List<PrecioEspecialidad> GetAll();
        PrecioEspecialidad Add(PrecioEspecialidad x);
        PrecioEspecialidad Update(PrecioEspecialidad x);
        void Delete(long Id);
        PrecioEspecialidad GetByEspecialidadAndTipoSeguro(long especialidadId, long tipoSeguroId);
        bool Repetido(long especialidadId, long tipoSeguroId);
    }
}
