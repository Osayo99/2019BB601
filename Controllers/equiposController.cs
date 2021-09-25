using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using _2019BB601.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _2019BB601.Controllers
{
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly _2019BB601Context _contexto;

        public equiposController(_2019BB601Context miContexto){
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/equipos")]
        public IActionResult Get(){
            IEnumerable<equipos> equiposList = from e in _contexto.equipos
                                               select e;

            if (equiposList.Count() > 0)
            {
                return Ok(equiposList);
            }
            return NotFound();
        }
        
        [HttpGet]
        [Route("api/equipos/{id}")]
        public IActionResult getbyId(int id)
        {
            equipos unEquipo = (from e in _contexto.equipos
                                where e.id_equipos == id
                                select e).FirstOrDefault();
            if (unEquipo != null)
            {
                return Ok(unEquipo);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/equipos/buscanombre/{nombre}")]

        public IActionResult obtenerNombre(string nombre)
        {
            IEnumerable<equipos> equiposPorNombre = from e in _contexto.equipos
                                                    where e.nombre.Contains(nombre)
                                                    select e;
            if (equiposPorNombre.Count() > 0)
            {
                return Ok(equiposPorNombre);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("api/equipos/insertar")]

        public IActionResult insertarEquipo([FromBody] equipos equipoNuevo)
        {
            try
            {
                IEnumerable<equipos> equipoExiste = from e in _contexto.equipos
                                                    where e.nombre == equipoNuevo.nombre
                                                    select e;
                if(equipoExiste.Count()==0)
                {
                    _contexto.equipos.Add(equipoNuevo);
                    _contexto.SaveChanges();
                    return Ok(equipoNuevo);
                } 
                 return Ok(equipoExiste);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/equipos/modificar")]
        public IActionResult updateEquipo([FromBody]  equipos equipoModificar)
        {
            equipos equipoExiste = (from e in _contexto.equipos
                                    where e.id_equipos == equipoModificar.id_equipos
                                    select e).FirstOrDefault();
            if(equipoExiste is null)
            {
                return NotFound();
            }

            equipoExiste.nombre = equipoModificar.nombre;
            equipoExiste.descripcion = equipoModificar.descripcion;
            equipoExiste.modelo = equipoModificar.modelo;

            _contexto.Entry(equipoExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(equipoExiste);
        }
    }
}