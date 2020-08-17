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
        // GET: Tramite
        public ActionResult Index(int id_Tramite)
        {
            ViewBag.Id_Tramite = id_Tramite;
            return View(conexion.Tipo_Tramites.ToList());
        }
        [HttpGet]
        public ActionResult Buscar(string query, int id_Tramite)
        {
            ViewBag.Id_Tramite = id_Tramite;
            ViewBag.datos = query;
            List<Tipo_Tramite> datos;
            if (query != null)
            {
                datos = conexion.Tipo_Tramites.Where(a => a.Nombre.Contains(query)).ToList();
                return View(datos);
            }
            return View(conexion.Tramites.ToList());
        }
        [HttpGet]
        public ActionResult Crear(int id_Tramite)
        {
            ViewBag.Id_Tramite = id_Tramite;
            return View(new Tipo_Tramite());
        }
        [HttpPost]
        public ActionResult Crear(Tipo_Tramite tipo_tramite, int id_Tramite)
        {
            ViewBag.Id_Tramite = id_Tramite;
            Validar(tipo_tramite);
            if (ModelState.IsValid == true)
            {
                conexion.Tipo_Tramites.Add(tipo_tramite);
                conexion.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_tramite);
        }
        [HttpGet]
        public ActionResult Editar(int id, int id_Tramite)
        {
            ViewBag.Id_Tramite = id_Tramite;
            var tipo_tramite = conexion.Tipo_Tramites.Find(id);
            return View(tipo_tramite);
        }
        [HttpPost]
        public ActionResult Editar(Tipo_Tramite tipo_tramite, int id, int id_Tramite)
        {
            ViewBag.Id_Tramite = id_Tramite;
            Validar(tipo_tramite);
            if (ModelState.IsValid == true)
            {
                var tipo_tramiteDB = conexion.Tipo_Tramites.Find(id);
                tipo_tramiteDB.Nombre = tipo_tramite.Nombre;
                tipo_tramiteDB.Descripcion = tipo_tramite.Descripcion;
                conexion.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_tramite);
        }

        [HttpGet]
        public ActionResult Eliminar(int id, int id_Tramite)
        {
            ViewBag.Id_Tramite = id_Tramite;
            var tipo_tramiteDB = conexion.Tipo_Tramites.Find(id);
            conexion.Tipo_Tramites.Remove(tipo_tramiteDB);
            conexion.SaveChanges();
            return RedirectToAction("Index");
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