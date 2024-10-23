using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IDALs
{
    public interface IDAL_Especialidades
    {
        Especialidades GetEspecialidadById(int id);
        IEnumerable<Especialidades> GetAllEspecialidades();
        void AddEspecialidad(Especialidades especialidad);
        void UpdateEspecialidad(Especialidades especialidad);
        void DeleteEspecialidad(int id);
    }
}
