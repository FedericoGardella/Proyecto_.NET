using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class ResultadosEstudios
    {
        public ResultadosEstudios() { }
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }

        [ForeignKey("HistoriasClinicas")]
        public long HistoriasClinicasId { get; set; }
        public HistoriasClinicas HistoriasClinicas { get; set; }

        public ResultadoEstudio GetEntity()
        {
            return new ResultadoEstudio
            {
                Id = Id,
                Descripcion = Descripcion,
                Fecha = Fecha,
                HistoriaClinicaId = HistoriasClinicasId,
                HistoriaClinica = HistoriasClinicas?.GetEntity()
            };
        }

        public static ResultadosEstudios FromEntity(ResultadoEstudio resultadoEstudio, ResultadosEstudios resultadosEstudios)
        {
            ResultadosEstudios resultadoEstudioToSave = resultadosEstudios ?? new ResultadosEstudios();

            resultadoEstudioToSave.Id = resultadoEstudio.Id;
            resultadoEstudioToSave.Descripcion = resultadoEstudio.Descripcion;
            resultadoEstudioToSave.Fecha = resultadoEstudio.Fecha;
            resultadoEstudioToSave.HistoriasClinicasId = resultadoEstudio.HistoriaClinicaId;

            return resultadoEstudioToSave;
        }
    }
}
