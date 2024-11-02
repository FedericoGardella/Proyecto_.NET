using System;
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
    public class Medicos : Personas
    {
        [Required]
        public string Matricula { get; set; }

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
