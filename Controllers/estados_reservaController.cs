using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using _2019BB601.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _2019BB601.Controllers
{
    [ApiController]
    public class estados_reservaController : ControllerBase
    {
        private readonly _2019BB601Context _contexto;

        public estados_reservaController(_2019BB601Context miContexto){
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/estadosres")]
        public IActionResult Get()
        {
            IEnumerable<estados_reserva> tipoList = from e in _contexto.estados_reserva
                                                select e;

            if (tipoList.Count() > 0)
            {
                return Ok(tipoList);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/estadosres/{id}")]
        public IActionResult getbyId(int id)
        {
            estados_reserva unEstadoRes = (from e in _contexto.estados_reserva
                                     where e.estados_res_id == id
                                     select e).FirstOrDefault();
            if (unEstadoRes != null)
            {
                return Ok(unEstadoRes);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/estadosres/buscarEstado/{buscarEstado}")]
        public IActionResult getDescrip(string buscarEstado)
        {
            IEnumerable<estados_reserva> estadoRes = from e in _contexto.estados_reserva
                                                        where e.estado.Contains(buscarEstado)
                                                       select e;
            if (estadoRes.Count() > 0)
            {
                return Ok(estadoRes);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("api/estadosres/insertar")]
        public IActionResult insertarEstado([FromBody] estados_reserva estadoReservaNuevo)
        {
            try
            {
                IEnumerable<estados_reserva> EstadoReservaExiste = from e in _contexto.estados_reserva
                                                         where e.estados_res_id == estadoReservaNuevo.estados_res_id
                                                      select e;
                if (EstadoReservaExiste.Count() == 0)
                {
                    _contexto.estados_reserva.Add(estadoReservaNuevo);
                    _contexto.SaveChanges();
                    return Ok(estadoReservaNuevo);
                }
                return BadRequest(EstadoReservaExiste);
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/estadosres/modificar")]
        public IActionResult updateEstados([FromBody] estados_reserva estadoReservaModificar)
        {
            estados_reserva estadoReservaExiste = (from e in _contexto.estados_reserva
                                                 where e.estados_res_id == estadoReservaModificar.estados_res_id
                                      select e).FirstOrDefault();
            if (estadoReservaExiste is null)
            {
                return NotFound();
            }

            estadoReservaExiste.estado = estadoReservaModificar.estado;


            _contexto.Entry(estadoReservaExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(estadoReservaExiste);
        }
    }
}