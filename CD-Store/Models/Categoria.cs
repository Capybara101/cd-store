using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CD_Store.Models
{
    internal class Categoria
    {
        public int idCategoria { get; set; }
        public string nombre { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaActualizacion { get; set; }
        public int estado { get; set; }
    }
}
