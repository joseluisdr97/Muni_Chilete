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
        public ActionResult Index()
        {
            return View(conexion.Tramites.ToList());
        }
        [HttpGet]
        public ActionResult Buscar(string query)
        {
            ViewBag.datos = query;
            List<Tramite> datos;
            if (query != null)
            {
                datos = conexion.Tramites.Where(a => a.Nombre.Contains(query)).ToList();
                return View(datos);
            }
            return View(conexion.Tramites.ToList());
        }
        [HttpGet]
        public ActionResult Crear()
        {
            return View(new Tramite());
        }
        [HttpPost]
        public ActionResult Crear(Tramite tramite)
        {
            Validar(tramite);
            if (ModelState.IsValid == true)
            {
                conexion.Tramites.Add(tramite);
                conexion.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tramite);
        }
        [HttpGet]
        public ActionResult Editar(int id)
        {
            var tramite = conexion.Tramites.Find(id);
            return View(tramite);
        }
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

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            var tramiteDB = conexion.Tramites.Find(id);
            conexion.Tramites.Remove(tramiteDB);
            conexion.SaveChanges();
            return RedirectToAction("Index");
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