using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Municipalidad_Chilete.Models
{
    public class Requisito
    {
        public int Id_Requisito { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Formato { get; set; }
        public int Id_Tipo_Tramite { get; set; }
        public Tipo_Tramite Tipo_Tramite { get; set; }
    }
}