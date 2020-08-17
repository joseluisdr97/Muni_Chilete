using Municipalidad_Chilete.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Municipalidad_Chilete.DB.Maps
{
    public class Tipo_TramiteMap : EntityTypeConfiguration<Tipo_Tramite>
    {
        public Tipo_TramiteMap()
        {
            ToTable("Tipo_Tramite");
            HasKey(o => o.Id_Tipo_Tramite);

            //Relaciones

            HasRequired(o => o.Tramite)
            .WithMany(o => o.Tipo_Tramites)
            .HasForeignKey(o => o.Id_Tramite);
        }
    }
}