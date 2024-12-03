using Shared.DTOs;
using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_TiposSeguros
    {
        TipoSeguro Get(long Id);
        List<TipoSeguro> GetAll();
        TipoSeguro Add(TipoSeguro x);
        TipoSeguro Update(TipoSeguro x);
        void Delete(long Id);
        Articulo UpdateCosto(long tipoSeguroId, decimal nuevoCosto);
        decimal GetCostoArticulo(long tipoSeguroId);
        //List<ContratoSeguroDTO> GetContratosSeguros(long TipoSeguroId);
    }
}
