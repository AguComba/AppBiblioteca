using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppBiblioteca.Data;
using AppBiblioteca.Models;

namespace AppBiblioteca.Controllers
{
    public class EditorialesController : Controller
    {
        private AppBibliotecaContext db = new AppBibliotecaContext();

        // GET: Editoriales
        public ActionResult Index(string MensajeDevuelto)
        {
            ViewBag.MensajeDevuelto = MensajeDevuelto;
            return View(db.Editoriales.ToList());
        }

        // GET: Editoriales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Editoriales editoriales = db.Editoriales.Find(id);
            if (editoriales == null)
            {
                return HttpNotFound();
            }
            return View(editoriales);
        }

        // GET: Editoriales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Editoriales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EditorialID,EditorialNombre")] Editoriales editoriales)
        {
            if (ModelState.IsValid)
            {
                db.Editoriales.Add(editoriales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(editoriales);
        }

        // GET: Editoriales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Editoriales editoriales = db.Editoriales.Find(id);
            if (editoriales == null)
            {
                return HttpNotFound();
            }
            return View(editoriales);
        }

        // POST: Editoriales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EditorialID,EditorialNombre")] Editoriales editoriales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editoriales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editoriales);
        }

        // GET: Editoriales/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Editoriales editoriales = db.Editoriales.Find(id);
        //    if (editoriales == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(editoriales);
        //}

        // POST: Editoriales/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var libro = (from a in db.Libros where a.EditorialID == id select a).Count(); 
            var mensajeDevuelto = "";

            if (libro > 0)
            {
                mensajeDevuelto= "No se puede eliminar la Editorial seleccionada debido a que esta relacionada a un Libro.";
            }
            else
            {
                Editoriales editoriales = db.Editoriales.Find(id);
                db.Editoriales.Remove(editoriales);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { MensajeDevuelto = mensajeDevuelto});
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
