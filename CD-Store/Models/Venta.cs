using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CD_Store.Models
{
    internal class Venta
    {
        public int idVenta { get; set; }
        public double total { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaActualizacion { get; set; }
        public int estado { get; set; }
    }
}
