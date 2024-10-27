using Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace DAL.Models

{
    public class Pacientes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombres { get; set; }

        [Required]
        [MaxLength(150)]
        public string Apellidos { get; set; }

        [Required]
        [MaxLength(20), MinLength(4)]
        public string Telefono { get; set; }

        [Required]
        [MaxLength(8),MinLength(8)]
        public string Cedula { get; set; }


        public Paciente GetEntity()
        {
            Paciente paciente = new Paciente();

            paciente.Id = Id;
            paciente.Nombres = Nombres;
            paciente.Apellidos = Apellidos;
            paciente.Telefono = Telefono;
            paciente.Cedula = Cedula;

            return paciente;
        }

        public static Pacientes FromEntity(Paciente paciente, Pacientes pacientes)
        {
            Pacientes pacienteToSave;
            if (pacientes == null)
                pacienteToSave = new Pacientes();
            else
                pacienteToSave = pacientes;

            pacienteToSave.Id = paciente.Id;
            pacienteToSave.Nombres = paciente.Nombres;
            pacienteToSave.Apellidos = paciente.Apellidos;
            pacienteToSave.Telefono = paciente.Telefono;
            pacienteToSave.Cedula = paciente.Cedula;

            return pacienteToSave;
        }
    }
}
