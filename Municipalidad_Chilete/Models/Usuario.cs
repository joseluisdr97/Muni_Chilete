using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Municipalidad_Chilete.Models
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
    }
}