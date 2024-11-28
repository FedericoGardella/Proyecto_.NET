using Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Medicos : Personas
    {

        public Medicos() { }

        [Required]
        public string Matricula { get; set; }

        public List<Especialidades> Especialidades { get; set; } = new List<Especialidades>();
        public List<GruposCitas> GruposCitas { get; set; }


        public Medico GetEntity()
        {
            return new Medico
            {
                Id = Id,
                Nombres = Nombres,
                Apellidos = Apellidos,
                Documento = Documento,
                Matricula = Matricula
            };
        }

        public static Medicos FromEntity(Medico medico, Medicos medicos)
        {
            Medicos medicoToSave = medicos ?? new Medicos();

            medicoToSave.Id = medico.Id;
            medicoToSave.Nombres = medico.Nombres;
            medicoToSave.Apellidos = medico.Apellidos;
            medicoToSave.Documento = medico.Documento;
            medicoToSave.Matricula = medico.Matricula;

            return medicoToSave;
        }
    }
}
