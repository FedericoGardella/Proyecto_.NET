using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Articulos
    {
        Articulo Get(long Id);
        List<Articulo> GetAll();
        Articulo Add(Articulo x);
        Articulo Update(Articulo x);
        void Delete(long Id);
        Articulo UpdateCosto(long tipoSeguroId, decimal nuevoCosto);
    }
}
