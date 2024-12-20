﻿using DAL.Models;
using Shared.Entities;

namespace DAL.IDALs
{
    public interface IDAL_Pacientes
    {
        Paciente Get(long Id);
        List<Paciente> GetAll();
        Paciente Add(Paciente x);
        Paciente Update(Paciente x);
        public void Delete(long Id);
        Paciente GetPacienteByDocumento(string documento);

    }
}
