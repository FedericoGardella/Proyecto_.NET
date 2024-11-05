using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_Recetas
    {
        Receta Get(long Id);
        List<Receta> GetAll();
        Receta Add(Receta x);
        Receta Update(Receta x);
        void Delete(long Id);
    }
}
