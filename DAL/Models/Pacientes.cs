﻿using Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Pacientes : Personas
    {
        [Required]
        [MaxLength(20), MinLength(4)]
        public string Telefono { get; set; }

        public List<Citas> Citas { get; set; }

        [ForeignKey("ContratosSeguros")]
        public long? ContratosSegurosId { get; set; }
        public ContratosSeguros? ContratosSeguros { get; set; }

        [ForeignKey("HistoriasClinicas")]
        public long? HistoriasClinicasId { get; set; }  
        public HistoriasClinicas? HistoriasClinicas { get; set; }

        public Paciente GetEntity()
        {
            return new Paciente
            {
                Id = Id,
                Nombres = Nombres,
                Apellidos = Apellidos,
                Telefono = Telefono,
                Documento = Documento,
                HistoriaClinicaId = HistoriasClinicasId,
                ContratoSeguroId = ContratosSegurosId
            };
        }

        public static Pacientes FromEntity(Paciente paciente, Pacientes pacientes)
        {
            Pacientes pacienteToSave = pacientes ?? new Pacientes();

            pacienteToSave.Id = paciente.Id;
            pacienteToSave.Nombres = paciente.Nombres;
            pacienteToSave.Apellidos = paciente.Apellidos;
            pacienteToSave.Telefono = paciente.Telefono;
            pacienteToSave.Documento = paciente.Documento;
            pacienteToSave.HistoriasClinicasId = paciente.HistoriaClinicaId;
            pacienteToSave.ContratosSegurosId = paciente.ContratoSeguroId;

            return pacienteToSave;
        }
    }
}
