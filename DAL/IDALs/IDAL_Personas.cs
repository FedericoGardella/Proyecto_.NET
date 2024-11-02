using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IDALs
{
    public interface IDAL_Personas
    {
        Persona Get(long Id);
        List<Persona> GetAll();
        Persona GetXDocumento(string Documento);
        Persona Add(Persona x);
        Persona Update(Persona x);
        void Delete(long Id);
    }
}
