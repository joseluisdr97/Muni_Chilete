using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Municipalidad_Chilete.Models
{
    public class Tipo_Tramite
    {
        public int Id_Tipo_Tramite { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Id_Tramite { get; set; }
        public Tramite Tramite { get; set; }
        public bool Activo_Inactivo { get; set; }
        public List<Requisito> Requisitos { get; set; }
    }
}