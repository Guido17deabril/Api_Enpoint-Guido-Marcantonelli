using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Enpoint.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public string Comentarios { get; set; }
        public int CantidadVentas { get; set; }
    }
}