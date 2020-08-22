using Municipalidad_Chilete.DB;
using Municipalidad_Chilete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Municipalidad_Chilete.Controllers
{
    public class ConvocatoriaController : Controller
    {
        private AppConexionDB conexion = new AppConexionDB();
        // GET: Tramite
        public ActionResult Index()
        {
            return View(conexion.Convocatorias.ToList());
        }
        [HttpGet]
        public ActionResult Buscar(string query)
        {
            ViewBag.datos = query;
            List<Convocatoria> datos;
            if (query != null)
            {
                datos = conexion.Convocatorias.Where(a => a.Descripcion.Contains(query)).ToList();
                return View(datos);
            }
            return View(conexion.Convocatorias.ToList());
        }
        [HttpGet]
        public ActionResult Crear()
        {
            return View(new Tramite());
        }
        [HttpPost]
        public ActionResult Crear(Convocatoria convocatoria)
        {
            Validar(convocatoria);
            if (ModelState.IsValid == true)
            {
                conexion.Convocatorias.Add(convocatoria);
                conexion.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(convocatoria);
        }
        [HttpGet]
        public ActionResult Editar(int id)
        {
            var convocatoria = conexion.Convocatorias.Find(id);
            return View(convocatoria);
        }
        [HttpPost]
        public ActionResult Editar(Convocatoria convocatoria, int id)
        {
            Validar(convocatoria);
            if (ModelState.IsValid == true)
            {
                var convocatoriaDB = conexion.Convocatorias.Find(id);
                convocatoriaDB.Descripcion = convocatoria.Descripcion;
                conexion.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(convocatoria);
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            var convocatoriaDB = conexion.Convocatorias.Find(id);
            conexion.Convocatorias.Remove(convocatoriaDB);
            conexion.SaveChanges();
            return RedirectToAction("Index");
        }

        private void Validar(Convocatoria convocatoria)
        {
            if (convocatoria.Descripcion == null)
                ModelState.AddModelError("Descripcion", "El campo descripcion es obligatorio ");
        }
    }
}