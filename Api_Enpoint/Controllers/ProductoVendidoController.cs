using Api_Enpoint.Models;
using Api_Enpoint.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api_Enpoint.Controllers
{
    public class ProductoVendidoController : ApiController
    {
        private List<ProductoVendido> productosVendidos; // Lista de productoVendido

        public ProductoVendidoController()
        {
            productosVendidos = new List<ProductoVendido>(); // Inicializar la lista de productosVendidos
        }

        [HttpGet]
        public IEnumerable<ProductoVendido> GetProductoVendidos()
        {
            return ADO_ProductoVendido.ObtenerProductosVendidos();
        }

        [HttpPut]
        public IHttpActionResult ActualizarProductoVendido(int id, [FromBody] ProductoVendido productoVendido)
        {
            if (productoVendido == null)
            {
                // Si el objeto productoVendido es nulo, devuelve un código de estado BadRequest
                return BadRequest("El objeto productoVendido no puede ser nulo.");
            }

            // Busca el productoVendido existente en la lista por su ID
            ProductoVendido productoVendidoExistente = productosVendidos.FirstOrDefault(u => u.Id == id);

            if (productoVendidoExistente == null)
            {
                // Si no se encuentra el productoVendido, devuelve un código de estado NotFound
                return NotFound();
            }

            // Actualiza los datos del productoVendido existente con los datos proporcionados
            productoVendidoExistente.IdVenta = productoVendido.IdVenta;
            productoVendidoExistente.Id = productoVendido.Id;
            productoVendidoExistente.IdProducto = productoVendido.IdProducto;
            productoVendidoExistente.Stock = productoVendido.Stock;

            // Devuelve un código de estado NoContent para indicar que la actualización se realizó correctamente
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public void EliminarProductoVendido(int id)
        {
            ADO_ProductoVendido.EliminarProductosVendidosId(id);
        }
 

    }
}
