using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Recetas
    {
        Receta Get(long Id);
        List<Receta> GetAll();
        Receta Add(Receta x);
        Receta Update(Receta x);
        void Delete(long Id);
    }
}
