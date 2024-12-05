using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Especialidades
    {
        Especialidad Get(long Id);
        List<Especialidad> GetByIds(List<long> ids);
        List<Especialidad> GetAll();
        Especialidad Add(Especialidad x);
        Especialidad Update(Especialidad x);
        void Delete(long Id);
        //Especialidad GetEspecialidad(long Id, string token);
        List<Especialidad> GetEspecialidadesPorMedico(long medicoId);

    }
}
