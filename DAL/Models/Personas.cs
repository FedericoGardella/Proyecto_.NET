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
            Persona persona = new Persona
            {
                Id = Id,
                Nombres = Nombres,
                Apellidos = Apellidos,
                Documento = Documento
            };

            if (this is Pacientes pacientes)
            {
                persona = new Paciente
                {
                    Id = pacientes.Id,
                    Nombres = pacientes.Nombres,
                    Apellidos = pacientes.Apellidos,
                    Documento = pacientes.Documento,
                    Telefono = pacientes.Telefono,
                    HistoriasClinicas = pacientes.HistoriasClinicas?.Select(h => h.GetEntity()).ToList(),
                    ContratosSeguros = pacientes.ContratosSeguros?.Select(c => c.GetEntity()).ToList(),
                    Facturas = pacientes.Facturas?.Select(f => f.GetEntity()).ToList(),
                };
            }
            else if (this is Medicos medicos)
            {
                persona = new Medico
                {
                    Id = medicos.Id,
                    Nombres = medicos.Nombres,
                    Apellidos = medicos.Apellidos,
                    Documento = medicos.Documento,
                    Matricula = medicos.Matricula,
                    Especialidades = medicos.Especialidades?.Select(e => e.GetEntity()).ToList()
                };
            }

            return persona;
        }


        public static Personas FromEntity(Persona persona, Personas personas = null)
        {
            Personas personaToSave = personas ?? new Personas
            {
                Id = persona.Id,
                Nombres = persona.Nombres,
                Apellidos = persona.Apellidos,
                Documento = persona.Documento
            };

            if (persona is Paciente paciente)
            {
                if (personaToSave is Pacientes pacientes)
                {
                    pacientes.Telefono = paciente.Telefono;
                    pacientes.HistoriasClinicas = paciente.HistoriasClinicas?.Select(h => HistoriasClinicas.FromEntity(h, null)).ToList();
                    pacientes.ContratosSeguros = paciente.ContratosSeguros?.Select(c => ContratosSeguros.FromEntity(c, null)).ToList();
                    pacientes.Facturas = paciente.Facturas?.Select(f => Facturas.FromEntity(f, null)).ToList();
                }
            }
            else if (persona is Medico medico)
            {
                if (personaToSave is Medicos medicos)
                {
                    medicos.Matricula = medico.Matricula;
                    medicos.Especialidades = medico.Especialidades?.Select(e => Especialidades.FromEntity(e, null)).ToList();
                }
            }

            return personaToSave;
        }
    }
}
