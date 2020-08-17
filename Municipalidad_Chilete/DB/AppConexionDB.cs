using Municipalidad_Chilete.DB.Maps;
using Municipalidad_Chilete.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Municipalidad_Chilete.DB
{
    public class AppConexionDB : DbContext
    {
        public IDbSet<Tramite> Tramites { get; set; }
        public IDbSet<Tipo_Tramite> Tipo_Tramites { get; set; }
        public IDbSet<Requisito> Requisitos { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new TramiteMap());
            modelBuilder.Configurations.Add(new Tipo_TramiteMap());
            modelBuilder.Configurations.Add(new RequisitoMap());
        }
    }
}