﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class EspecialidadUpdateDTO
    {
        [MaxLength(100)]
        public string? Nombre { get; set; }

        public TimeSpan? TiempoCita { get; set; }
    }
}
