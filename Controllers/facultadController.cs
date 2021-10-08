using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using _2019BB601.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _2019BB601.Controllers
{
    [ApiController]
    public class facultadController : ControllerBase
    {
        private readonly _2019BB601Context _contexto;

        public facultadController(_2019BB601Context miContexto){
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/facultades")]
        public IActionResult Get()
        {
            IEnumerable<facultades> facultadList = from e in _contexto.facultades
                                               select e;

            if (facultadList.Count() > 0)
            {
                return Ok(facultadList);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/facultades/{id}")]
        public IActionResult getbyId(int id)
        {
            facultades unaFacultad = (from e in _contexto.facultades
                               where e.facultad_id == id
                               select e).FirstOrDefault();
            if (unaFacultad != null)
            {
                return Ok(unaFacultad);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/facultad/buscarFacultad/{nombre}")]
        public IActionResult buscarFacultad(string nombre)
        {
            IEnumerable<facultades> facultadPorNombre = from e in _contexto.facultades
                                                    where e.nombre_facultad.Contains(nombre)
                                                   select e;
            if (facultadPorNombre.Count() > 0)
            {
                return Ok(facultadPorNombre);
            }

            return NotFound();
        }


        [HttpPost]
        [Route("api/facultad/insertar")]
        public IActionResult nuevaFacultad([FromBody] facultades facultadNueva)
        {
            try
            {
                IEnumerable<facultades> facultadExiste = from e in _contexto.facultades
                                                    where e.nombre_facultad == facultadNueva.nombre_facultad
                                                  select e;
                if (facultadExiste.Count() == 0)
                {
                    _contexto.facultades.Add(facultadNueva);
                    _contexto.SaveChanges();
                    return Ok(facultadNueva);
                }
                return BadRequest(facultadExiste);
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/facultad/modificar")]
        public IActionResult updateFacultad([FromBody] facultades facultadModificar)
        {
            facultades facultadExiste = (from e in _contexto.facultades
                                    where e.facultad_id == facultadModificar.facultad_id
                                    select e).FirstOrDefault();
            if (facultadExiste is null)
            {
                return NotFound();
            }

            facultadExiste.nombre_facultad = facultadModificar.nombre_facultad;
         

            _contexto.Entry(facultadExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(facultadExiste);
        }
    }
}