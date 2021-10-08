using System;
using System.ComponentModel.DataAnnotations;
namespace _2019BB601.Models
{
    public class facultad
    {
        [Key]
        public int facultad_id { get; set; }
        public string nombre_facultad { get; set; }
    }
}