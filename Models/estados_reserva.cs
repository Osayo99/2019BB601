using System;
using System.ComponentModel.DataAnnotations;
namespace _2019BB601.Models
{
    public class estados_reserva
    {
        [Key]
        public int estados_res_id { get; set; }
        public string estado { get; set; }
    }
}