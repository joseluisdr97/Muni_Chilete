using Municipalidad_Chilete.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Municipalidad_Chilete.DB.Maps
{
    public class ConvocatoriaMap : EntityTypeConfiguration<Convocatoria>
    {
        public ConvocatoriaMap()
        {
            ToTable("Convocatoria");
            HasKey(o => o.Id_Convocatoria);
        }
    }
}