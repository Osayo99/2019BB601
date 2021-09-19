using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using _2019BB601.Models;

namespace _2019BB601.Controllers
{
    [ApiController]
    public class tipo_equipoController : ControllerBase
    {
        private readonly _2019BB601Context _contexto;

        public tipo_equipoController(_2019BB601Context miContexto){
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/tipo")]
        public IActionResult Get(){
        var tipo_equipoList = _contexto.tipo_equipo;
        return Ok(tipo_equipoList);
        }
    }
}