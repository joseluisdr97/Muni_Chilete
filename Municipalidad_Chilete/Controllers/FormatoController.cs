using Municipalidad_Chilete.DB;
using Municipalidad_Chilete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Municipalidad_Chilete.Controllers
{
    public class FormatoController : Controller
    {
        private AppConexionDB conexion = new AppConexionDB();
        // GET: Tramite
        [Authorize]
        public ActionResult Index()
        {
            return View(conexion.Formatos.ToList());
        }
        [Authorize]
        [HttpGet]
        public ActionResult Buscar(string query)
        {
            ViewBag.datos = query;
            List<Formato> datos;
            if (query != null)
            {
                datos = conexion.Formatos.Where(a => a.Nombre.Contains(query)).ToList();
                return View(datos);
            }
            return View(conexion.Formatos.ToList());
        }
        [Authorize]
        [HttpGet]
        public ActionResult Crear()
        {
            return View(new Formato());
        }
        [Authorize]
        [HttpPost]
        public ActionResult Crear(Formato formato)
        {
            Validar(formato);
            if (ModelState.IsValid == true)
            {
                conexion.Formatos.Add(formato);
                conexion.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formato);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Editar(int id)
        {
            var formato = conexion.Formatos.Find(id);
            return View(formato);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Editar(Formato formato, int id)
        {

            Validar(formato);
            if (ModelState.IsValid == true)
            {
                var formatoDB = conexion.Formatos.Find(id);
                formatoDB.Nombre = formato.Nombre;
                formatoDB.Url = formato.Url;
                conexion.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formato);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            var formatoDB = conexion.Formatos.Find(id);
            conexion.Formatos.Remove(formatoDB);
            conexion.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Formato()
        {
            return View(conexion.Formatos.ToList());
        }

        private void Validar(Formato formato)
        {
            if (formato.Nombre == null)
                ModelState.AddModelError("Nombre", "El campo nombre es obligatorio");
            if (formato.Url == null)
                ModelState.AddModelError("Url", "El campo url es obligatorio ");
        }
    }
}