﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Shared.Entities
{
    public class Paciente : Persona
    {
<<<<<<< HEAD
        public long Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Documento { get; set; }
        public string Telefono { get; set; }
        public HistoriaClinica historiaClinica { get; set; }
=======
        public string Telefono { get; set; }
        public string Cedula { get; set; }
>>>>>>> Fede
    }
}
