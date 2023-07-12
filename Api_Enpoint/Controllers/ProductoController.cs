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
    public class ProductoController : ApiController
    {
        private List<Producto> productos; // Lista de productos

        public ProductoController()
        {
            productos = new List<Producto>(); // Inicializar la lista de productos
        }

        [HttpGet]
        public IEnumerable<Producto> GetProductos()
        {
            return ADO_Producto.ObtenerProductos();
        }

        // Actualiza un producto existente        
        [HttpPut]
        public IHttpActionResult ActualizarProducto(int id, [FromBody] Producto producto)
        {
            if (producto == null)
            {
                // Si el objeto producto es nulo, devuelve un código de estado BadRequest
                return BadRequest("El objeto producto no puede ser nulo.");
            }

            // Busca el producto existente en la lista por su ID
            Producto productoExistente = productos.FirstOrDefault(u => u.Id == id);

            if (productoExistente == null)
            {
                // Si no se encuentra el producto, devuelve un código de estado NotFound
                return NotFound();
            }

            // Actualiza los datos del producto existente con los datos proporcionados
            productoExistente.Id = producto.Id;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.Costo = producto.Costo;
            productoExistente.PrecioVenta = producto.PrecioVenta;
            productoExistente.Stock = producto.Stock;
            productoExistente.IdUsuario = producto.IdUsuario;

            // Devuelve un código de estado NoContent para indicar que la actualización se realizó correctamente
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public void Eliminar([FromBody] int id)
        {
            ADO_Producto.EliminarProductoId(id);
        }

        [HttpPost]
        public IHttpActionResult CrearVenta([FromBody] Producto producto)
        {
            if (producto == null)
            {
                // Si el objeto producto es nulo, devuelve un código de estado BadRequest
                return BadRequest("El objeto producto no puede ser nulo.");
            }

            // Asigna el siguiente ID disponible al nuevo producto
            producto.Id = ObtenerSiguienteId();

            // Agrega el nuevo producto a la lista
            productos.Add(producto);

            // Devuelve un código de estado Created y la ruta para acceder al nuevo producto
            return CreatedAtRoute("DefaultApi", new { id = producto.Id }, producto);
        }

        private int ObtenerSiguienteId()
        {
            // Obtiene el ID máximo de los productos existentes y le suma 1 para obtener el siguiente ID
            return productos.Max(u => u.Id) + 1;
        }

    }
}
