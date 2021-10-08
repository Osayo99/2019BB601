using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using _2019BB601.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _2019BB601.Controllers
{
    [ApiController]
    public class carrerasController : ControllerBase
    {
        private readonly _2019BB601Context _contexto;

        public carrerasController(_2019BB601Context miContexto){
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/carreras")]
        public IActionResult Get()
        {
            IEnumerable<facultad> carrerasList = from e in _contexto.carreras
                                               select e;

            if (carrerasList.Count() > 0)
            {
                return Ok(carrerasList);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/carreras/{id}")]
        public IActionResult getbyId(int id)
        {
            carreras unaCarrera = (from e in _contexto.carreras
                               where e.carrera_id == id
                               select e).FirstOrDefault();
            if (unaCarrera != null)
            {
                return Ok(unaCarrera);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/carreras/buscarCarrera/{nombre}")]
        public IActionResult buscarCarrera(string nombre)
        {
            IEnumerable<carreras> carrerasPorNombre = from e in _contexto.carreras
                                                    where e.nombre_carrera.Contains(nombre)
                                                   select e;
            if (carrerasPorNombre.Count() > 0)
            {
                return Ok(carrerasPorNombre);
            }

            return NotFound();
        }


        [HttpPost]
        [Route("api/carreras/insertar")]
        public IActionResult nuevaCarrera([FromBody] carreras carreraNueva)
        {
            try
            {
                IEnumerable<carreras> carreraExiste = from e in _contexto.carreras
                                                    where e.nombre_carrera == carreraNueva.nombre_carrera
                                                  select e;
                if (carreraExiste.Count() == 0)
                {
                    _contexto.carreras.Add(carreraNueva);
                    _contexto.SaveChanges();
                    return Ok(carreraNueva);
                }
                return BadRequest(carreraExiste);
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/carreras/modificar")]
        public IActionResult updateCarrera([FromBody] carreras carreraModificar)
        {
            carreras carreraExiste = (from e in _contexto.carreras
                                    where e.carrera_id == carreraModificar.carrera_id
                                    select e).FirstOrDefault();
            if (carreraExiste is null)
            {
                return NotFound();
            }

            carreraExiste.nombre_carrera = carreraModificar.nombre_carrera;
            carreraExiste.estados = carreraModificar.estados;
         

            _contexto.Entry(carreraExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(carreraExiste);
        }
    }
}