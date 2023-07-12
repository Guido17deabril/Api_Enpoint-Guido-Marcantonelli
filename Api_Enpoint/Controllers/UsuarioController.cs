using Api_Enpoint.Models;
using Api_Enpoint.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;

namespace Api_Enpoint.Controllers
{
    public class UsuarioController : ApiController
    {
        private List<Usuario> usuarios; // Lista de usuarios

        public UsuarioController()
        {
            usuarios = new List<Usuario>(); // Inicializar la lista de usuarios
        }

        [HttpGet]
        public IEnumerable<Usuario> GetUsuarios()
        {
            return ADO_Usuario.GetUsuarios();

        }

        [HttpPut]
        public IHttpActionResult ActualizarUsuario(int id, [FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                // Si el objeto usuario es nulo, devuelve un código de estado BadRequest
                return BadRequest("El objeto usuario no puede ser nulo.");
            }

            // Busca el usuario existente en la lista por su ID
            Usuario usuarioExistente = usuarios.FirstOrDefault(u => u.Id == id);

            if (usuarioExistente == null)
            {
                // Si no se encuentra el usuario, devuelve un código de estado NotFound
                return NotFound();
            }

            // Actualiza los datos del usuario existente con los datos proporcionados
            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Apellido = usuario.Apellido;

            // Devuelve un código de estado NoContent para indicar que la actualización se realizó correctamente
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
