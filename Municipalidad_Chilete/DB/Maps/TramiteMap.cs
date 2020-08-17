using Municipalidad_Chilete.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Municipalidad_Chilete.DB.Maps
{
    public class TramiteMap : EntityTypeConfiguration<Tramite>
    {
        public TramiteMap()
        {
            ToTable("Tramite");
            HasKey(o => o.Id_Tramite);

        }
    }
}