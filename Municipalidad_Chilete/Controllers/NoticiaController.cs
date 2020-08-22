using Municipalidad_Chilete.DB;
using Municipalidad_Chilete.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Municipalidad_Chilete.Controllers
{
    public class NoticiaController : Controller
    {
        private AppConexionDB conexion = new AppConexionDB();
        // GET: Tramite
        public ActionResult Index()
        {
            return View(conexion.Noticias.ToList());
        }
        [HttpGet]
        public ActionResult Buscar(string query)
        {
            ViewBag.datos = query;
            List<Noticia> datos;
            if (query != null)
            {
                datos = conexion.Noticias.Where(a => a.Descripcion.Contains(query)).ToList();
                return View(datos);
            }
            return View(conexion.Noticias.ToList());
        }
        [HttpGet]
        public ActionResult Crear()
        {
            return View(new Noticia());
        }
        [HttpPost]
        public ActionResult Crear(Noticia noticia, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string ruta = Path.Combine(Server.MapPath("~/Imagenes"), Path.GetFileName(file.FileName));
                file.SaveAs(ruta);
                noticia.Imagen = "/Imagenes/" + Path.GetFileName(file.FileName);
            }
            Validar(noticia);
            if (ModelState.IsValid == true)
            {
                conexion.Noticias.Add(noticia);
                conexion.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noticia);
        }
        [HttpGet]
        public ActionResult Editar(int id)
        {
            var noticia = conexion.Noticias.Find(id);
            return View(noticia);
        }
        [HttpPost]
        public ActionResult Editar(Noticia noticia, int id, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string ruta = Path.Combine(Server.MapPath("~/Imagenes"), Path.GetFileName(file.FileName));
                file.SaveAs(ruta);
                noticia.Imagen = "/Imagenes/" + Path.GetFileName(file.FileName);
            }

            ValidarEditar(noticia);
            if (ModelState.IsValid == true)
            {
                var noticiaDB = conexion.Noticias.Find(id);
                noticiaDB.Descripcion = noticia.Descripcion;
                if (noticia.Imagen != null)
                {
                    noticiaDB.Imagen = noticia.Imagen;
                }
                conexion.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noticia);
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            var noticiaDB = conexion.Noticias.Find(id);
            conexion.Noticias.Remove(noticiaDB);
            conexion.SaveChanges();
            return RedirectToAction("Index");
        }

        private void Validar(Noticia noticia)
        {
            if (noticia.Imagen == null)
                ModelState.AddModelError("Imagen", "El campo imagen es obligatorio");
            if (noticia.Descripcion == null)
                ModelState.AddModelError("Descripcion", "El campo descripcion es obligatorio ");
        }
        private void ValidarEditar(Noticia noticia)
        {
            if (noticia.Descripcion == null)
                ModelState.AddModelError("Descripcion", "El campo descripcion es obligatorio ");
        }
    }
}