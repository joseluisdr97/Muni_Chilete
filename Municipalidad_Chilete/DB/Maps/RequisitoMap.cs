using Municipalidad_Chilete.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Municipalidad_Chilete.DB.Maps
{
    public class RequisitoMap : EntityTypeConfiguration<Requisito>
    {
        public RequisitoMap()
        {
            ToTable("Requisito");
            HasKey(o => o.Id_Requisito);

            HasRequired(o => o.Tipo_Tramite)
           .WithMany(o => o.Requisitos)
           .HasForeignKey(o => o.Id_Tipo_Tramite);
        }
    }
}