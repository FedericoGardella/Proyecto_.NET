﻿using DAL.IDALs;
using DAL.Models;
using Shared.DTOs;
using Shared.Entities;

namespace DAL.DALs
{
    public class DAL_HistoriasClinicas_Service : IDAL_HistoriasClinicas
    {
        public HistoriaClinica Update(HistoriaClinica historiaClinica)
        {
            throw new NotImplementedException();
        }

        public HistoriaClinica Add(HistoriaClinica historiaClinica)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public HistoriaClinica Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<HistoriaClinica> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<DiagnosticoDTO> GetDiagnosticos(long historiaClinicaId)
        {
            throw new NotImplementedException();
        }

        public List<ResultadoEstudio> GetResultadoEstudios(long historiaClinicaId)
        {
            throw new NotImplementedException();
        }

        public List<Receta> GetRecetas(long historiaClinicaId)
        {
            throw new NotImplementedException();
        }

        public List<HistoriaClinicaDTO> GetHistoriasByDocumento(string documento)
        {
            throw new NotImplementedException();
        }
    }
}
