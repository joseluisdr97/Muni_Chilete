using Municipalidad_Chilete.DB;
using Municipalidad_Chilete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Municipalidad_Chilete.Controllers
{
    public class LoginController : Controller
    {
        private AppConexionDB conexion = new AppConexionDB();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            var UExiste = conexion.Usuarios.Count(u => u.Correo == usuario.Correo && u.Password == usuario.Password);

            if (UExiste != 0)
            {
                var UsuarioDB = conexion.Usuarios.First(u => u.Correo == usuario.Correo && u.Password == usuario.Password);
                FormsAuthentication.SetAuthCookie(UsuarioDB.Correo, false);

                Session["NombreUsuario"] = UsuarioDB.Nombre;
                Session["CorreoUsuario"] = UsuarioDB.Correo;
                Session["PasswordUsuario"] = UsuarioDB.Password;

                return RedirectToAction("Index", "Tramite");
            }
            ViewBag.Validation = "Usuario y/o contraseña incorrecta";
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("login");
        }
    }
}