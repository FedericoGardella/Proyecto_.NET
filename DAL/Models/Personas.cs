using Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Personas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombres { get; set; }

        [Required]
        [MaxLength(100)]
        public string Apellidos { get; set; }

        public string Documento { get; set; }

        public Persona GetEntity()
        {
            Persona persona = new Persona();

            persona.Id = Id;
            persona.Nombres = Nombres;
            persona.Apellidos = Apellidos;
            persona.Documento = Documento;

            return persona;
        }

        public static Personas FromEntity(Persona persona, Personas personas = null)
        {
            Personas personaToSave = personas ?? new Personas();

            personaToSave.Id = persona.Id;
            personaToSave.Nombres = persona.Nombres;
            personaToSave.Apellidos = persona.Apellidos;
            personaToSave.Documento = persona.Documento;

            if (persona is Paciente paciente)
            {
                if (personaToSave is Pacientes pacientes)
                {
                    pacientes.Telefono = paciente.Telefono;
                    pacientes.HistoriasClinicasId = paciente.HistoriaClinicaId;
                    
                }
            }

            return personaToSave;
        }

    }
}
