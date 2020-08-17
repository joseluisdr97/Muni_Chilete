using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Municipalidad_Chilete.Models
{
    public class Tramite
    {
        public int Id_Tramite { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<Tipo_Tramite> Tipo_Tramites { get; set; }
    }
}