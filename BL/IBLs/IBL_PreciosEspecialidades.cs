using Shared.Entities;
using Shared.DTOs;

namespace BL.IBLs
{
    public interface IBL_PreciosEspecialidades
    {
        PrecioEspecialidad Get(long Id);
        List<PrecioEspecialidadNombresDTO> GetAll();
        PrecioEspecialidad Add(PrecioEspecialidad x);
        PrecioEspecialidad Update(PrecioEspecialidad x);
        void Delete(long Id);
        Articulo UpdateCosto(long precioEspecialidadId, decimal nuevoCosto);
        decimal GetCosto(long especialidadId, long tipoSeguroId);
        bool Repetido(long especialidadId, long tipoSeguroId);

    }
}
