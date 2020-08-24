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
        [Authorize]
        public ActionResult Index()
        {
            return View(conexion.Noticias.ToList());
        }
        [Authorize]
        [HttpGet]
        public ActionResult Buscar(string query)
        {
            ViewBag.datos = query;
            List<Noticia> datos;
            if (query != null)
            {
                datos = conexion.Noticias.Where(a => a.Titulo.Contains(query)).ToList();
                return View(datos);
            }
            return View(conexion.Noticias.ToList());
        }
        [Authorize]
        [HttpGet]
        public ActionResult Crear()
        {
            return View(new Noticia());
        }
        [Authorize]
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
        [Authorize]
        [HttpGet]
        public ActionResult Editar(int id)
        {
            var noticia = conexion.Noticias.Find(id);
            return View(noticia);
        }
        [Authorize]
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
                noticiaDB.Titulo = noticia.Titulo;
                if (noticia.Imagen != null)
                {
                    noticiaDB.Imagen = noticia.Imagen;
                }
                conexion.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noticia);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            var noticiaDB = conexion.Noticias.Find(id);
            conexion.Noticias.Remove(noticiaDB);
            conexion.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Noticia()
        {
            return View(conexion.Noticias.ToList());
        }

        private void Validar(Noticia noticia)
        {
            if (noticia.Imagen == null)
                ModelState.AddModelError("Imagen", "El campo imagen es obligatorio");
            if (noticia.Descripcion == null)
                ModelState.AddModelError("Descripcion", "El campo descripcion es obligatorio ");
            if (noticia.Titulo == null)
                ModelState.AddModelError("Titulo", "El campo titulo es obligatorio ");
        }
        private void ValidarEditar(Noticia noticia)
        {
            if (noticia.Descripcion == null)
                ModelState.AddModelError("Descripcion", "El campo descripcion es obligatorio ");
            if (noticia.Titulo == null)
                ModelState.AddModelError("Titulo", "El campo titulo es obligatorio ");
        }
    }
}