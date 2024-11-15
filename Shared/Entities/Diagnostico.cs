﻿namespace Shared.Entities
{
    public class Diagnostico
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public long HistoriaClinicaId { get; set; }
        public HistoriaClinica HistoriaClinica { get; set; }
    }
}
