using Municipalidad_Chilete.DB;
using Municipalidad_Chilete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Municipalidad_Chilete.Controllers
{
    public class RequisitoController : Controller
    {
        private AppConexionDB conexion = new AppConexionDB();
        // GET: Tramite
        [Authorize]
        public ActionResult Index(int id_Tipo_Tramite)
        {
            ViewBag.Tramite = conexion.Tipo_Tramites.Find(id_Tipo_Tramite);
            ViewBag.Tipo_Tramite = conexion.Tipo_Tramites.Find(id_Tipo_Tramite);

            return View(conexion.Requisitos.Where(a => a.Id_Tipo_Tramite == id_Tipo_Tramite).ToList());
        }
        [Authorize]
        [HttpGet]
        public ActionResult Buscar(string query, int id_Tipo_Tramite)
        {
            ViewBag.Tipo_Tramite = conexion.Tipo_Tramites.Find(id_Tipo_Tramite);
            ViewBag.Tramite = conexion.Tipo_Tramites.Find(id_Tipo_Tramite);
            ViewBag.datos = query;
            List<Requisito> datos;
            if (query != null)
            {
                datos = conexion.Requisitos.Where(a => a.Descripcion.Contains(query) && a.Id_Tipo_Tramite == id_Tipo_Tramite).ToList();
                return View(datos);
            }
            return View(conexion.Requisitos.Where(a => a.Id_Tipo_Tramite == id_Tipo_Tramite).ToList());
        }
        [Authorize]
        [HttpGet]
        public ActionResult Crear(int id_Tipo_Tramite)
        {
            ViewBag.Tipo_Tramite = conexion.Tipo_Tramites.Find(id_Tipo_Tramite);
            return View(new Requisito());
        }
        [Authorize]
        [HttpPost]
        public ActionResult Crear(Requisito requisito, int id_Tipo_Tramite)
        {
            ViewBag.Tipo_Tramite = conexion.Tipo_Tramites.Find(id_Tipo_Tramite);
            Validar(requisito);
            if (ModelState.IsValid == true)
            {
                conexion.Requisitos.Add(requisito);
                conexion.SaveChanges();
                return RedirectToAction("Index", new { id_Tipo_Tramite = id_Tipo_Tramite });
            }
            return View(requisito);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Editar(int id, int id_Tipo_Tramite)
        {
            ViewBag.Tipo_Tramite = conexion.Tipo_Tramites.Find(id_Tipo_Tramite);
            var requisito = conexion.Requisitos.Find(id);
            return View(requisito);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Editar(Requisito requisito, int id, int id_Tipo_Tramite)
        {
            ViewBag.Tipo_Tramite = conexion.Tipo_Tramites.Find(id_Tipo_Tramite);
            Validar(requisito);
            if (ModelState.IsValid == true)
            {
                var requisitoDB = conexion.Requisitos.Find(id);
                requisitoDB.Descripcion = requisito.Descripcion;
                requisitoDB.Precio = requisito.Precio;
                requisitoDB.Formato = requisito.Formato;
                conexion.SaveChanges();
                return RedirectToAction("Index", new { id_Tipo_Tramite = id_Tipo_Tramite });
            }
            return View(requisito);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Eliminar(int id, int id_Tipo_Tramite)
        {
            ViewBag.Tipo_Tramite = conexion.Tipo_Tramites.Find(id_Tipo_Tramite);
            var requisitoDB = conexion.Requisitos.Find(id);
            conexion.Requisitos.Remove(requisitoDB);
            conexion.SaveChanges();
            return RedirectToAction("Index", new { id_Tipo_Tramite = id_Tipo_Tramite });
        }

        private void Validar(Requisito requisito)
        {
            if (requisito.Descripcion == null)
                ModelState.AddModelError("Descripcion", "El campo descripcion es obligatorio");
            if (requisito.Precio < 0)
                ModelState.AddModelError("Precio", "El campo precio es obligatorio");
            if (requisito.Formato ==  null)
                ModelState.AddModelError("Formato", "El campo formato es obligatorio");
        }
    }
}