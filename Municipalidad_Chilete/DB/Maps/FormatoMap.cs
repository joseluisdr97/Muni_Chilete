using Municipalidad_Chilete.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Municipalidad_Chilete.DB.Maps
{
    public class FormatoMap : EntityTypeConfiguration<Formato>
    {
        public FormatoMap()
        {
            ToTable("Formato");
            HasKey(o => o.Id_Formato);

        }
    }
}