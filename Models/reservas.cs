using System;
using System.ComponentModel.DataAnnotations;
namespace _2019BB601.Models
{
    public class reservas
    {
        [Key]
        public int reserva_id { get; set; }
        public int equipo_id { get; set; }
        public int usuario_id { get; set; }
        public string fecha_salida { get; set; }
        public string hora_salida { get; set; }
        public int tiempo_reserva { get; set; }
        public int estado_reserva_id { get; set; }
        public string fecha_retorno { get; set; }
        public string hora_retorno { get; set; }
    }
}