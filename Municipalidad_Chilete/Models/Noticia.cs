using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Municipalidad_Chilete.Models
{
    public class Noticia
    {
        public int Id_Noticia { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public string Titulo { get; set; }

    }
}