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

        public ResultadoEstudio GetEntity()
        {
            ResultadoEstudio resultadoEstudio = new ResultadoEstudio();

            resultadoEstudio.Id = Id;
            resultadoEstudio.Descripcion = Descripcion;
            resultadoEstudio.Fecha = Fecha;

            return resultadoEstudio;
        }

        public static ResultadosEstudios FromEntity(ResultadoEstudio resultadoEstudio, ResultadosEstudios resultadosEstudios)
        {
            ResultadosEstudios resultadoEstudioToSave;
            if (resultadosEstudios == null)
                resultadoEstudioToSave = new ResultadosEstudios();
            else
                resultadoEstudioToSave = resultadosEstudios;

            resultadoEstudioToSave.Id = resultadoEstudio.Id;
            resultadoEstudioToSave.Descripcion = resultadoEstudio.Descripcion;
            resultadoEstudioToSave.Fecha = resultadoEstudio.Fecha;

            return resultadoEstudioToSave;
        }
    }
}
