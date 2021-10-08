using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using _2019BB601.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _2019BB601.Controllers
{
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly _2019BB601Context _contexto;

        public usuariosController(_2019BB601Context miContexto){
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/usuarios")]
        public IActionResult Get(){
            IEnumerable<usuarios> usuariosList = from e in _contexto.usuarios
                                               select e;

            if (usuariosList.Count() > 0)
            {
                return Ok(usuariosList);
            }
            return NotFound();
        }
        
        [HttpGet]
        [Route("api/usuarios/{id}")]
        public IActionResult getbyId(int id)
        {
            usuarios unUsuario = (from e in _contexto.usuarios
                                where e.usuario_id == id
                                select e).FirstOrDefault();
            if (unUsuario != null)
            {
                return Ok(unUsuario);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/usuarios/buscanombre/{nombre}")]

        public IActionResult obtenerNombre(string nombre)
        {
            IEnumerable<usuarios> usuariosPorNombre = from e in _contexto.usuarios
                                                    where e.nombre.Contains(nombre)
                                                    select e;
            if (usuariosPorNombre.Count() > 0)
            {
                return Ok(usuariosPorNombre);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("api/usuarios/insertar")]

        public IActionResult insertarUsuario([FromBody] usuarios usuarioNuevo)
        {
            try
            {
                IEnumerable<usuarios> usuarioExiste = from e in _contexto.usuarios
                                                    where e.nombre == usuarioNuevo.nombre
                                                    select e;
                if(usuarioExiste.Count()==0)
                {
                    _contexto.usuarios.Add(usuarioNuevo);
                    _contexto.SaveChanges();
                    return Ok(usuarioNuevo);
                } 
                 return Ok(usuarioExiste);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/usuarios/modificar")]
        public IActionResult updateEquipo([FromBody]  usuarios usuarioModificar)
        {
            usuarios usuarioExiste = (from e in _contexto.usuarios
                                    where e.usuario_id == usuarioModificar.usuario_id
                                    select e).FirstOrDefault();
            if(usuarioExiste is null)
            {
                return NotFound();
            }

            usuarioExiste.nombre = usuarioModificar.nombre;
            usuarioExiste.documento = usuarioModificar.documento;
            usuarioExiste.carnet = usuarioModificar.carnet;

            _contexto.Entry(usuarioExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(usuarioExiste);
        }
    }
}