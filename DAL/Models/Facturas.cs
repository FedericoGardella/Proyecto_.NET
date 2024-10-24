﻿using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Facturas
    {
        public int Id { get; set; }
        public DateTime FechaEmision { get; set; }
        public bool Pago { get; set; }

        //public List<Cita> Citas { get; set; }

        // public Factura()
        //{
        //    Citas = new List<Cita>();
        // }

        public Factura GetEntity()
        {
            Factura factura = new Factura();

            factura.Id = Id;
            factura.FechaEmision = FechaEmision;
            factura.Pago = Pago;

            return factura;
        }

        public static Facturas FromEntity(Factura factura, Facturas facturas)
        {
            Facturas facturaToSave;
            if (facturas == null)
                facturaToSave = new Facturas();
            else
                facturaToSave = facturas;

            facturaToSave.Id = factura.Id;
            facturaToSave.FechaEmision = factura.FechaEmision;
            facturaToSave.Pago = factura.Pago;

            return facturaToSave;
        }

    }
}
