using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_Articulos
    {
        Articulo Get(long Id);
        List<Articulo> GetAll();
        Articulo Add(Articulo x);
        Articulo Update(Articulo x);
        void Delete(long Id);
    }
}
