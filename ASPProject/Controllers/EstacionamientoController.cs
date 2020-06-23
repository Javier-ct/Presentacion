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
    public class EstacionamientoController : Controller
    {
        private ProyectoInacapEntities db = new ProyectoInacapEntities();

        // GET: Estacionamiento
        public ActionResult Index()
        {
            var estacionamiento = db.Estacionamiento.Include(e => e.Bicicleta).Include(e => e.Trabajador);
            return View(estacionamiento.ToList());
        }

        // GET: Estacionamiento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estacionamiento estacionamiento = db.Estacionamiento.Find(id);
            if (estacionamiento == null)
            {
                return HttpNotFound();
            }
            return View(estacionamiento);
        }

        // GET: Estacionamiento/Create
        public ActionResult Create()
        {
            ViewBag.idBicicleta = new SelectList(db.Bicicleta, "IdBicicleta", "Marca");
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "IdTrabajador", "Nombre");
            return View();
        }

        // POST: Estacionamiento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Estacionamiento estacionamiento)
        {
            if (ModelState.IsValid)             
            {
                Estacionamiento estacionamientoDB = db.Estacionamiento.Where(x => x.LugarEstacionamiento == estacionamiento.LugarEstacionamiento).FirstOrDefault();

                if (estacionamientoDB == null)
                {
                    db.Estacionamiento.Add(estacionamiento);
                    db.SaveChanges();
                    

                }
                else {
                   

                }

                
                return RedirectToAction("Index");

            }

            ViewBag.idBicicleta = new SelectList(db.Bicicleta, "IdBicicleta", "Marca", estacionamiento.idBicicleta);
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "IdTrabajador", "Nombre", estacionamiento.idTrabajador);
            return View(estacionamiento);
        }

        // GET: Estacionamiento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estacionamiento estacionamiento = db.Estacionamiento.Find(id);
            if (estacionamiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.idBicicleta = new SelectList(db.Bicicleta, "IdBicicleta", "Marca", estacionamiento.idBicicleta);
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "IdTrabajador", "Nombre", estacionamiento.idTrabajador);
            return View(estacionamiento);
        }

        // POST: Estacionamiento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEstacionamiento,LugarEstacionamiento,HoraEntrada,HoraSalida,EstacionamientoOcupado,idBicicleta,idTrabajador")] Estacionamiento estacionamiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estacionamiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idBicicleta = new SelectList(db.Bicicleta, "IdBicicleta", "Marca", estacionamiento.idBicicleta);
            ViewBag.idTrabajador = new SelectList(db.Trabajador, "IdTrabajador", "Nombre", estacionamiento.idTrabajador);
            return View(estacionamiento);
        }

        // GET: Estacionamiento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estacionamiento estacionamiento = db.Estacionamiento.Find(id);
            if (estacionamiento == null)
            {
                return HttpNotFound();
            }
            return View(estacionamiento);
        }

        // POST: Estacionamiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]


        public ActionResult DeleteConfirmed(int id)
        {


            Estacionamiento estacionamiento = db.Estacionamiento.Find(id);
            db.Estacionamiento.Remove(estacionamiento);
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
