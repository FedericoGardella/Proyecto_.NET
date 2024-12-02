using DAL.IDALs;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_PreciosEspecialidades_Service : IDAL_PreciosEspecialidades
    {
        public PrecioEspecialidad Update(PrecioEspecialidad precioEspecialidad)
        {
            throw new NotImplementedException();
        }

        public PrecioEspecialidad Add(PrecioEspecialidad precioEspecialidad)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public PrecioEspecialidad Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<PrecioEspecialidad> GetAll()
        {
            throw new NotImplementedException();
        }

        public PrecioEspecialidad GetByEspecialidadAndTipoSeguro(long especialidadId, long tipoSeguroId) 
        { 
            throw new NotImplementedException(); 
        }

        public bool Repetido(long especialidadId, long tipoSeguroId) 
        {  
            throw new NotImplementedException(); 
        }
    }
}
