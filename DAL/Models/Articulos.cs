﻿using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Articulos
    {
        public Articulos() { }
        public long Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }

        [ForeignKey("TiposSeguros")]
        public long TiposSegurosId { get; set; }
        public TiposSeguros TiposSeguros { get; set; }

        public PreciosEspecialidades PreciosEspecialidades { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Costo { get; set; }

        public Articulo GetEntity()
        {
            Articulo articulo = new Articulo();

            articulo.Id = Id;
            articulo.Nombre = Nombre;
            articulo.Fecha = Fecha;
            articulo.Costo = Costo;

            return articulo;
        }

        public static Articulos FromEntity(Articulo articulo, Articulos articulos)
        {
            Articulos articuloToSave;
            if (articulos == null)
                articuloToSave = new Articulos();
            else
                articuloToSave = articulos;

            articuloToSave.Id = articulo.Id;
            articuloToSave.Nombre = articulo.Nombre;
            articuloToSave.Fecha = articulo.Fecha;
            articuloToSave.Costo = articulo.Costo;

            return articuloToSave;
        }
    }
}
