using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IDALs
{
    internal class IDAL_Recetas
    {
        IEnumerable<Recetas> ObtenerRecetas();

        Recetas ObtenerRecetaPorId(int id);

        void AgregarReceta(Recetas receta);

        void ActualizarReceta(Recetas receta);

        void EliminarReceta(int id);
    }
}
