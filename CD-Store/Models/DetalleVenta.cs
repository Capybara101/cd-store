using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CD_Store.Models
{
    internal class DetalleVenta
    {
        public int idDetalleVenta { get; set; }
        public int idVenta { get; set; }
        public int idProducto { get; set; }
        public double precioUnitario { get; set; }
        public int cantidad { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaActualizacion { get; set; }
        public int estado { get; set; }
    }
}
