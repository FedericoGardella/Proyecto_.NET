using Shared.DTOs;
using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_GruposCitas
    {
        GrupoCita Get(long Id);
        List<GrupoCita> GetAll();
        GrupoCita GetGrupoCitasMedico(long medicoId, DateTime fecha);
        GrupoCita Add(GrupoCita x);
        GrupoCita AddGrupoCitaConCitas(GrupoCitaPostDTO dto);
        List<GrupoCitaDTO> GetByEspecialidadAndMes(long especialidadId, int mes);
        GrupoCita Update(GrupoCita x);
        void Delete(long Id);
    }
}
