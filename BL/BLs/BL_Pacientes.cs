using BL.IBLs;
using DAL.IDALs;
using Shared.Entities;

namespace BL.BLs
{
    public class BL_Pacientes : IBL_Pacientes
    {
        private readonly IDAL_Pacientes _dal;

        public BL_Pacientes(IDAL_Pacientes dal)
        {
            _dal = dal ?? throw new ArgumentNullException(nameof(dal));
        }

        public Paciente Get(long Id)
        {
            return _dal.Get(Id);
        }

        public List<Paciente> GetAll()
        {
            return _dal.GetAll();
        }

        public Paciente Add(Paciente x)
        {
            return _dal.Add(x);
        }

        public Paciente Update(Paciente x)
        {
            return _dal.Update(x);
        }

        public void Delete(long Id)
        {
            _dal.Delete(Id);
        }

        public Paciente GetPacienteByDocumento(string documento)
        {
            return _dal.GetPacienteByDocumento(documento);
        }
    }
}
