﻿using Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Especialidades
    {
        public Especialidades() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        public TimeSpan tiempoCita { get; set; }


        public List<Medicos> Medicos { get; set; }

        public Especialidad GetEntity()
        {
            Especialidad especialidad = new Especialidad();

            especialidad.Id = Id;
            especialidad.Nombre = Nombre;
            especialidad.tiempoCita = tiempoCita;

            return especialidad;
        }

        public static Especialidades FromEntity(Especialidad especialidad, Especialidades especialidades)
        {
            Especialidades especialidadToSave;
            if (especialidades == null)
                especialidadToSave = new Especialidades();
            else
                especialidadToSave = especialidades;

            especialidadToSave.Id = especialidad.Id;
            especialidadToSave.Nombre = especialidad.Nombre;
            especialidadToSave.tiempoCita = especialidad.tiempoCita;

            return especialidadToSave;
        }
    }
}
