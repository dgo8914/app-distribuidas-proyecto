using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Clases
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Profesor { get; set; }
        public int Cupo{ get; set; }
    }
}