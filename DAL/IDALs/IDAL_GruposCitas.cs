﻿using Shared.DTOs;
using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_GruposCitas
    {
        GrupoCita Get(long Id);
        List<GrupoCita> GetAll();
        GrupoCita GetGrupoCitasMedico(long medicoId, DateTime fecha);
        GrupoCita Add(GrupoCita x);
        GrupoCita AddGrupoCitaConCitas(GrupoCitaPostDTO dto);
        GrupoCita Update(GrupoCita x);
        void Delete(long Id);
    }
}
