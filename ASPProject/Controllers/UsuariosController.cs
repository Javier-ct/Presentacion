using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Conexion.Models;

namespace ASPProject.Controllers
{
    public class UsuariosController : Controller
    {
       

        private ProyectoInacapEntities db = new ProyectoInacapEntities();



        public ActionResult Administrador() {


            return PartialView();
        }



        public ActionResult Registro()
        {


            return PartialView();
        }

        public ActionResult Login()
        {

            return PartialView();
        }

        [HttpPost]
        public ActionResult Login(string User, string Pass)
        {
            try
            {

                var oUser = (from d in db.Usuario
                             where d.CorreoUsuario == User.Trim() && d.Contraseña == Pass.Trim()
                             select d).FirstOrDefault();
                if (oUser == null)
                {
                    ViewBag.Error = "Usuario o contraseña invalida";
                    return View();
                }

                Session["User"] = oUser.NombreUsuario;
                Session["Rol"] = oUser.RolUsuario;



                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }

        public ActionResult cerrar() {


            Session["User"] = null;
            Session["Rol"] = null;

            return RedirectToAction("Index","Home");
        }



        public ActionResult JJ(string Nombre,string Apellidos,int Telefono,string Direccion,int rut,string email,string pass1,string pass2) {


           

            if (ModelState.IsValid)
            {
                Usuario usuarioDB = new Usuario();

                if (pass1 == pass2)
                {
                    usuarioDB.NombreUsuario = Nombre;
                    usuarioDB.ApellidoUsuario = Apellidos;
                    usuarioDB.TelefonoUsuario = Telefono;
                    usuarioDB.DireccionUsuario = Direccion;
                    usuarioDB.RutUsuario = rut;
                    usuarioDB.FechaRegistro = DateTime.Now;
                    usuarioDB.Contraseña = pass1;
                    usuarioDB.CorreoUsuario = email;
                    usuarioDB.RolUsuario = "Cliente";

                 
                    

                    db.Usuario.Add(usuarioDB);

                    Session["TDO"] = usuarioDB;
                    Session["User"] = Nombre;
                    Session["Rol"] = "Cliente";
                    Session["ID"] = usuarioDB.IdUsuario;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");

                }



            }
            else
            {
                return RedirectToAction("Index", "Home");
            }



            return RedirectToAction("Index", "Home");


        }



















        // GET: Usuarios
        public ActionResult Index()
        {
            return View(db.Usuario.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUsuario,NombreUsuario,ApellidoUsuario,TelefonoUsuario,DireccionUsuario,RutUsuario,FechaRegistro,CorreoUsuario,Contraseña,RolUsuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsuario,NombreUsuario,ApellidoUsuario,TelefonoUsuario,DireccionUsuario,RutUsuario,FechaRegistro,CorreoUsuario,Contraseña,RolUsuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
