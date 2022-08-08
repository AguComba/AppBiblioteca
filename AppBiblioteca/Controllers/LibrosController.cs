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
    public class LibrosController : Controller
    {
        private AppBibliotecaContext db = new AppBibliotecaContext();

        // GET: Libros
        public ActionResult Index()
        {
            var libros = db.Libros.Include(l => l.Autores).Include(l => l.Editoriales).Include(l => l.Generos).Include(l => l.Secciones);
            return View(libros.ToList());
        }

        // GET: Libros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libros libros = db.Libros.Find(id);
            if (libros == null)
            {
                return HttpNotFound();
            }
            return View(libros);
        }

        // GET: Libros/Create
        public ActionResult Create()
        {
            ViewBag.AutoresID = new SelectList(db.Autores, "AutoresID", "AutoresNombreCompleto");
            ViewBag.EditorialID = new SelectList(db.Editoriales, "EditorialID", "EditorialNombre");
            ViewBag.GeneroID = new SelectList(db.Generos, "GeneroID", "GeneroNombre");
            ViewBag.SeccionesID = new SelectList(db.Secciones, "SeccionesID", "SeccionesNombre");
            return View();
        }

        // POST: Libros/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LibroID,LibroISBN,LibroTitulo,LibroResenia,LibroFechaPublicacion,EstadoLibro,AutoresID,EditorialID,GeneroID,SeccionesID")] Libros libros)
        {
            if (ModelState.IsValid)
            {
                db.Libros.Add(libros);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AutoresID = new SelectList(db.Autores, "AutoresID", "AutoresNombreCompleto", libros.AutoresID);
            ViewBag.EditorialID = new SelectList(db.Editoriales, "EditorialID", "EditorialNombre", libros.EditorialID);
            ViewBag.GeneroID = new SelectList(db.Generos, "GeneroID", "GeneroNombre", libros.GeneroID);
            ViewBag.SeccionesID = new SelectList(db.Secciones, "SeccionesID", "SeccionesNombre", libros.SeccionesID);
            return View(libros);
        }

        // GET: Libros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libros libros = db.Libros.Find(id);
            if (libros == null)
            {
                return HttpNotFound();
            }
            ViewBag.AutoresID = new SelectList(db.Autores, "AutoresID", "AutoresNombreCompleto", libros.AutoresID);
            ViewBag.EditorialID = new SelectList(db.Editoriales, "EditorialID", "EditorialNombre", libros.EditorialID);
            ViewBag.GeneroID = new SelectList(db.Generos, "GeneroID", "GeneroNombre", libros.GeneroID);
            ViewBag.SeccionesID = new SelectList(db.Secciones, "SeccionesID", "SeccionesNombre", libros.SeccionesID);
            return View(libros);
        }

        // POST: Libros/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LibroID,LibroISBN,LibroTitulo,LibroResenia,LibroFechaPublicacion,EstadoLibro,AutoresID,EditorialID,GeneroID,SeccionesID")] Libros libros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(libros).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AutoresID = new SelectList(db.Autores, "AutoresID", "AutoresNombreCompleto", libros.AutoresID);
            ViewBag.EditorialID = new SelectList(db.Editoriales, "EditorialID", "EditorialNombre", libros.EditorialID);
            ViewBag.GeneroID = new SelectList(db.Generos, "GeneroID", "GeneroNombre", libros.GeneroID);
            ViewBag.SeccionesID = new SelectList(db.Secciones, "SeccionesID", "SeccionesNombre", libros.SeccionesID);
            return View(libros);
        }

        // GET: Libros/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Libros libros = db.Libros.Find(id);
        //    if (libros == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(libros);
        //}

        // POST: Libros/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Libros libros = db.Libros.Find(id);
            db.Libros.Remove(libros);
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
