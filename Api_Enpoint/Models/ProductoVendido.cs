using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Enpoint.Models
{
    public class ProductoVendido
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int Stock { get; set; }
        public int IdVenta { get; set; }
    }
}