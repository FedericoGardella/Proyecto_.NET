using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.IBLs
{
    public interface IBL_Especialidades
    {
        Especialidades GetEspecialidadById(int id);
        IEnumerable<Especialidades> GetAllEspecialidades();
        void AddEspecialidad(Especialidades especialidad);
        void UpdateEspecialidad(Especialidades especialidad);
        void DeleteEspecialidad(int id);
    }
}
