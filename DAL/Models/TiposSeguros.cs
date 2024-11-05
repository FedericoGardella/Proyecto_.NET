using Shared.Entities;

namespace DAL.Models
{
    public class TiposSeguros
    {
        public TiposSeguros() { }
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public List<Articulos> Articulos { get; set; }


        public TipoSeguro GetEntity()
        {
            TipoSeguro tipoSeguro = new TipoSeguro();

            tipoSeguro.Id = Id;
            tipoSeguro.Nombre = Nombre;
            tipoSeguro.Descripcion = Descripcion;

            return tipoSeguro;
        }

        public static TiposSeguros FromEntity(TipoSeguro tipoSeguro, TiposSeguros tiposSeguros)
        {
            TiposSeguros tipoSeguroToSave;
            if (tiposSeguros == null)
                tipoSeguroToSave = new TiposSeguros();
            else
                tipoSeguroToSave = tiposSeguros;

            tipoSeguroToSave.Id = tipoSeguro.Id;
            tipoSeguroToSave.Nombre = tipoSeguro.Nombre;
            tipoSeguroToSave.Descripcion = tipoSeguro.Descripcion;

            return tipoSeguroToSave;
        }
    }
}
