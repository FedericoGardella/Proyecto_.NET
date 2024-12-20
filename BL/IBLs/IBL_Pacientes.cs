﻿using DAL.Models;
using Shared.Entities;

namespace BL.IBLs
{
    public interface IBL_Pacientes
    {
        Paciente Get(long Id);
        List<Paciente> GetAll();
        Paciente Add(Paciente x);
        Paciente Update(Paciente x);
        void Delete(long Id);
        Paciente GetPacienteByDocumento(string documento);
    }
}