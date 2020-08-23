using Municipalidad_Chilete.DB;
using Municipalidad_Chilete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Municipalidad_Chilete.Controllers
{
    public class TramiteController : Controller
    {
        private AppConexionDB conexion = new AppConexionDB();
        // GET: Tramite
        [Authorize]
        public ActionResult Index()
        {
            return View(conexion.Tramites.Where(a => a.Activo_Inactivo == true).ToList());
        }
        [Authorize]
        [HttpGet]
        public ActionResult Buscar(string query)
        {
            ViewBag.datos = query;
            List<Tramite> datos;
            if (query != null)
            {
                datos = conexion.Tramites.Where(a => a.Nombre.Contains(query) && a.Activo_Inactivo==true).ToList();
                return View(datos);
            }
            return View(conexion.Tramites.Where(a=>a.Activo_Inactivo==true).ToList());
        }
        [Authorize]
        [HttpGet]
        public ActionResult Crear()
        {
            return View(new Tramite());
        }
        [Authorize]
        [HttpPost]
        public ActionResult Crear(Tramite tramite)
        {
            Validar(tramite);
            if (ModelState.IsValid == true)
            {
                tramite.Activo_Inactivo = true;
                conexion.Tramites.Add(tramite);
                conexion.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tramite);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Editar(int id)
        {
            var tramite = conexion.Tramites.Find(id);
            return View(tramite);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Editar(Tramite tramite, int id)
        {
            Validar(tramite);
            if (ModelState.IsValid == true)
            {
                var tramiteDB = conexion.Tramites.Find(id);
                tramiteDB.Nombre = tramite.Nombre;
                tramiteDB.Descripcion = tramite.Descripcion;
                conexion.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tramite);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            var tramiteDB = conexion.Tramites.Find(id);
            tramiteDB.Activo_Inactivo = false;
            conexion.SaveChanges();
            return RedirectToAction("Index");
        }
        

        public ActionResult Tramite()
        {
            var datos = conexion.Tramites.Where(a => a.Activo_Inactivo == true).ToList();
            return View(datos);
        }
        public ActionResult TipoTramite(int id)
        {
            
            return View(conexion.Tipo_Tramites.Where(a=>a.Id_Tramite==id && a.Activo_Inactivo==true).ToList());
        }
        public ActionResult Requisito(int id)
        {
            ViewBag.Formatos = conexion.Formatos.ToList();
            return View(conexion.Requisitos.Where(a => a.Id_Tipo_Tramite == id).ToList());
        }

        private void Validar(Tramite tramite)
        {
            if (tramite.Nombre==null)
                ModelState.AddModelError("Nombre", "El campo nombre es obligatorio");
            if (tramite.Descripcion == null)
                ModelState.AddModelError("Descripcion", "El campo descripcion es obligatorio ");
        }
    }
}