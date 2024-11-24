﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class HistoriaClinicaDTO
    {
        public long Id { get; set; }
        public long PacienteId { get; set; }
        public string PacienteNombres { get; set; }
        public string PacienteApellidos { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public string Comentarios { get; set; }
        public string NombreMedico { get; set; }
        public long CitaId { get; set; }
    }
}
