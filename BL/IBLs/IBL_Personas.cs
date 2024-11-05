using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Personas
    {
        Persona Get(long Id);
        List<Persona> GetAll();
        Persona GetXDocumento(string Documento);
        Persona Add(Persona x);
        Persona Update(Persona x);
        void Delete(long Id);
    }
}
