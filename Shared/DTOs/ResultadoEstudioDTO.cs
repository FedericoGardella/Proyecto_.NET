using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class ResultadoEstudioDTO
    {
        public long? Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public long HistoriaClinicaId { get; set; }
    }
}
