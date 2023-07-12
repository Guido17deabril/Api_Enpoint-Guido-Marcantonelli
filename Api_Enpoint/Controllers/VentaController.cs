using Api_Enpoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api_Enpoint.Controllers
{
    public class VentaController : ApiController
    {
        private List<Venta> ventas; // Lista de ventas

        public VentaController()
        {
            ventas = new List<Venta>(); // Inicializar la lista de ventas
        }

        [HttpGet]
        public List<Venta> GetVentas()
        {
            return ADO_Venta.ObtenerVentas();
        }

        [HttpPost]
        public IHttpActionResult CrearVenta([FromBody] Venta venta)
        {
            if (venta == null)
            {
                // Si el objeto venta es nulo, devuelve un código de estado BadRequest
                return BadRequest("El objeto usuario no puede ser nulo.");
            }

            // Asigna el siguiente ID disponible al nuevo venta
            venta.Id = ObtenerSiguienteId();

            // Agrega el nuevo venta a la lista
            ventas.Add(venta);

            // Devuelve un código de estado Created y la ruta para acceder al nuevo venta
            return CreatedAtRoute("DefaultApi", new { id = venta.Id }, venta);
        }
        // Obtiene el siguiente ID disponible para un nuevo venta
        private int ObtenerSiguienteId()
        {
            // Obtiene el ID máximo de los ventas existentes y le suma 1 para obtener el siguiente ID
            return ventas.Max(u => u.Id) + 1;
        }
    }
}
