using Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Medicos : Personas
    {

        public Medicos() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        [Required]
        public string Matricula { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombres { get; set; }

        [Required]
        [MaxLength(100)]
        public string Apellidos { get; set; }

        public List<Especialidades> Especialidades { get; set; }
        public List<GruposCitas> GruposCitas { get; set; }


        public Medico GetEntity()
        {
            return new Medico
            {
                Id = Id,
                Nombres = Nombres,
                Apellidos = Apellidos,
                Matricula = Matricula
            };
        }

        public static Medicos FromEntity(Medico medico, Medicos medicos)
        {
            Medicos medicoToSave = medicos ?? new Medicos();

            medicoToSave.Id = medico.Id;
            medicoToSave.Nombres = medico.Nombres;
            medicoToSave.Apellidos = medico.Apellidos;
            medicoToSave.Matricula = medico.Matricula;

            return medicoToSave;
        }
    }
}
