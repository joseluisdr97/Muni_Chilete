using Municipalidad_Chilete.DB;
using Municipalidad_Chilete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Municipalidad_Chilete.Controllers
{
    public class Tipo_TramiteController : Controller
    {
        private AppConexionDB conexion = new AppConexionDB();

        [Authorize]
        // GET: Tramite
        public ActionResult Index(int id_Tramite)
        {
            ViewBag.Tramite = conexion.Tramites.Find(id_Tramite);
            return View(conexion.Tipo_Tramites.Where(a=>a.Id_Tramite==id_Tramite).ToList());
        }
        [Authorize]
        [HttpGet]
        public ActionResult Buscar(string query, int id_Tramite)
        {
            ViewBag.Tramite = conexion.Tramites.Find(id_Tramite);
            ViewBag.datos = query;
            List<Tipo_Tramite> datos;
            if (query != null)
            {
                datos = conexion.Tipo_Tramites.Where(a => a.Nombre.Contains(query) && a.Id_Tramite == id_Tramite).ToList();
                return View(datos);
            }
            return View(conexion.Tipo_Tramites.Where(a => a.Id_Tramite == id_Tramite).ToList());
        }
        [Authorize]
        [HttpGet]
        public ActionResult Crear(int id_Tramite)
        {
            ViewBag.Tramite = conexion.Tramites.Find(id_Tramite);
            return View(new Tipo_Tramite());
        }
        [Authorize]
        [HttpPost]
        public ActionResult Crear(Tipo_Tramite tipo_tramite, int id_Tramite)
        {
            ViewBag.Tramite = conexion.Tramites.Find(id_Tramite);
            Validar(tipo_tramite);
            if (ModelState.IsValid == true)
            {
                conexion.Tipo_Tramites.Add(tipo_tramite);
                conexion.SaveChanges();
                return RedirectToAction("Index", new { id_Tramite = id_Tramite });
            }
            return View(tipo_tramite);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Editar(int id, int id_Tramite)
        {
            ViewBag.Tramite = conexion.Tramites.Find(id_Tramite);
            var tipo_tramite = conexion.Tipo_Tramites.Find(id);
            return View(tipo_tramite);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Editar(Tipo_Tramite tipo_tramite, int id, int id_Tramite)
        {
            ViewBag.Tramite = conexion.Tramites.Find(id_Tramite);
            Validar(tipo_tramite);
            if (ModelState.IsValid == true)
            {
                var tipo_tramiteDB = conexion.Tipo_Tramites.Find(id);
                tipo_tramiteDB.Nombre = tipo_tramite.Nombre;
                tipo_tramiteDB.Descripcion = tipo_tramite.Descripcion;
                conexion.SaveChanges();
                return RedirectToAction("Index", new { id_Tramite = id_Tramite });
            }
            return View(tipo_tramite);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Eliminar(int id, int id_Tramite)
        {
            ViewBag.Tramite = conexion.Tramites.Find(id_Tramite);
            var tipo_tramiteDB = conexion.Tipo_Tramites.Find(id);
            conexion.Tipo_Tramites.Remove(tipo_tramiteDB);
            conexion.SaveChanges();
            return RedirectToAction("Index", new { id_Tramite = id_Tramite });
        }

        private void Validar(Tipo_Tramite tipo_tramite)
        {
            if (tipo_tramite.Nombre == null)
                ModelState.AddModelError("Nombre", "El campo nombre es obligatorio");
            if (tipo_tramite.Descripcion == null)
                ModelState.AddModelError("Descripcion", "El campo descripcion es obligatorio ");
        }
    }
}