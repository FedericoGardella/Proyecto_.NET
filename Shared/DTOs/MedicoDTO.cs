﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class MedicoDTO
    {
        public long Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Documento { get; set; }
        public string Matricula { get; set; }
    }
}
