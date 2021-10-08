using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using _2019BB601.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _2019BB601.Controllers
{
    [ApiController]
    public class reservasController : ControllerBase
    {
        private readonly _2019BB601Context _contexto;

        public reservasController(_2019BB601Context miContexto){
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/reservas")]
        public IActionResult Get(){
            IEnumerable<reservas> reservasList = from e in _contexto.reservas
                                               select e;

            if (reservasList.Count() > 0)
            {
                return Ok(reservasList);
            }
            return NotFound();
        }
        
        [HttpGet]
        [Route("api/reservas/{id}")]
        public IActionResult getbyId(int id)
        {
            reservas unaReserva = (from e in _contexto.reservas
                                where e.reserva_id == id
                                select e).FirstOrDefault();
            if (unaReserva != null)
            {
                return Ok(unaReserva);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/reservas/buscasalida/{salida}")]

        public IActionResult obtenerSalida(string salida)
        {
            IEnumerable<reservas> reservasPorSalida = from e in _contexto.reservas
                                                    where e.fecha_salida.Contains(salida)
                                                    select e;
            if (reservasPorSalida.Count() > 0)
            {
                return Ok(reservasPorSalida);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("api/reservas/insertar")]

        public IActionResult insertarReserva([FromBody] reservas reservaNueva)
        {
            try
            {
                IEnumerable<reservas> reservaExiste = from e in _contexto.reservas
                                                    where e.fecha_salida == reservaNueva.fecha_salida
                                                    select e;
                if(reservaExiste.Count()==0)
                {
                    _contexto.reservas.Add(reservaNueva);
                    _contexto.SaveChanges();
                    return Ok(reservaNueva);
                } 
                 return Ok(reservaExiste);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/reservas/modificar")]
        public IActionResult updateReservas([FromBody]  reservas reservaModificar)
        {
            reservas reservaExiste = (from e in _contexto.reservas
                                    where e.reserva_id == reservaModificar.reserva_id
                                    select e).FirstOrDefault();
            if(reservaExiste is null)
            {
                return NotFound();
            }

            reservaExiste.hora_salida = reservaModificar.hora_salida;
            reservaExiste.tiempo_reserva = reservaModificar.tiempo_reserva;
            reservaExiste.fecha_retorno = reservaModificar.fecha_retorno;

            _contexto.Entry(reservaExiste).State = EntityState.Modified;
            _contexto.SaveChanges();

            return Ok(reservaExiste);
        }
    }
}