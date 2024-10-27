﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BCrypt.Net;
using Shared.Entities;

namespace DAL.Models
{
    public class Medicos
    {
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

        public Medico GetEntity()
        {
            Medico medico = new Medico();

            medico.Id = Id;
            medico.Nombres = Nombres;
            medico.Apellidos = Apellidos;
            medico.Matricula = Matricula;

            return medico;
        }

        public static Medicos FromEntity(Medico medico, Medicos medicos)
        {
            Medicos medicoToSave;
            if (medicos == null)
                medicoToSave = new Medicos();
            else
                medicoToSave = medicos;

            medicoToSave.Id = medico.Id;
            medicoToSave.Nombres = medico.Nombres;
            medicoToSave.Apellidos = medico.Apellidos;
            medicoToSave.Matricula = medico.Matricula ;

            return medicoToSave;
        }

    }
}
