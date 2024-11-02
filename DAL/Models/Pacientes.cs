using Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace DAL.Models
{
    public class Pacientes : Personas
    {
        [Required]
        [MaxLength(20), MinLength(4)]
        public string Telefono { get; set; }

        [Required]
        [MaxLength(8), MinLength(8)]
        public string Cedula { get; set; }

        public Paciente GetEntity()
        {
            return new Paciente
            {
                Id = Id,
                Nombres = Nombres,
                Apellidos = Apellidos,
                Telefono = Telefono,
                Cedula = Cedula
            };
        }

        public static Pacientes FromEntity(Paciente paciente, Pacientes pacientes)
        {
            Pacientes pacienteToSave = pacientes ?? new Pacientes();

            pacienteToSave.Id = paciente.Id;
            pacienteToSave.Nombres = paciente.Nombres;
            pacienteToSave.Apellidos = paciente.Apellidos;
            pacienteToSave.Telefono = paciente.Telefono;
            pacienteToSave.Cedula = paciente.Cedula;

            return pacienteToSave;
        }
    }
}
