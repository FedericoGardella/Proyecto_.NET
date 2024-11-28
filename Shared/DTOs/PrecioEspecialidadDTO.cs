﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class PrecioEspecialidadDTO
    {
        public long? Id { get; set; }
        public long TipoSeguroId { get; set; }
        public long EspecialidadId { get; set; }
        public decimal Costo { get; set; }
    }
}
