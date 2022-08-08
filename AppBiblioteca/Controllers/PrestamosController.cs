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
    public class PrestamosController : Controller
    {
        private AppBibliotecaContext db = new AppBibliotecaContext();

        // GET: Prestamos
        public ActionResult Index()
        {
            var prestamos = db.Prestamos.Include(p => p.Socios);
            return View(prestamos.ToList());
        }

        // GET: Prestamos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamos prestamos = db.Prestamos.Find(id);
            if (prestamos == null)
            {
                return HttpNotFound();
            }
            return View(prestamos);
        }

        // GET: Prestamos/Create
        public ActionResult Create()
        {
            ViewBag.SocioID = new SelectList(db.Socios, "SocioID", "SocioNombreCompleto");

            var librosCombo = (from a in db.Libros where a.EstadoLibro == EstadoLibro.Disponible select a).ToList();
            ViewBag.LibroID = new SelectList(librosCombo, "LibroID", "LibroTitulo");

            return View();
        }

        // POST: Prestamos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrestamoID,PrestamoFecha,PrestamoFechaDevolucion,SocioID")] Prestamos prestamos)
        {
            if (ModelState.IsValid)
            {
                using (var transaccion = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Prestamos.Add(prestamos);
                        db.SaveChanges();

                        var prestamosDetallesT = (from a in db.PrestamosDetallesT select a).ToList();
                        foreach (var item in prestamosDetallesT)
                        {
                            var libroGuardar = new PrestamosDetalle
                            {
                                LibroID = item.LibroID,
                                PrestamosID = prestamos.PrestamoID
                            };
                            db.PrestamosDetalle.Add(libroGuardar);
                            db.SaveChanges();
                        }
                        db.PrestamosDetallesT.RemoveRange(prestamosDetallesT);
                        db.SaveChanges();

                        transaccion.Commit();

                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                    }
                }


            }

            ViewBag.SocioID = new SelectList(db.Socios, "SocioID", "SocioNombreCompleto", prestamos.SocioID);

            var librosCombo = (from a in db.Libros where a.EstadoLibro == EstadoLibro.Disponible select a).ToList();
            ViewBag.LibroID = new SelectList(librosCombo, "LibroID", "LibroTitulo");

            return View(prestamos);
        }

        //// GET: Prestamos/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Prestamos prestamos = db.Prestamos.Find(id);
        //    if (prestamos == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.SocioID = new SelectList(db.Socios, "SocioID", "SocioNombreCompleto", prestamos.SocioID);
        //    ViewBag.LibroID = new SelectList(db.Libros, "LibroID", "LibroTitulo");
        //    return View(prestamos);
        //}

        //// POST: Prestamos/Edit/5
        //// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        //// más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "PrestamoID,PrestamoFecha,PrestamoFechaDevolucion,SocioID")] Prestamos prestamos)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(prestamos).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.SocioID = new SelectList(db.Socios, "SocioID", "SocioNombreCompleto", prestamos.SocioID);
        //    ViewBag.LibroID = new SelectList(db.Libros, "LibroID", "LibroTitulo");
        //    return View(prestamos);
        //}

        // GET: Prestamos/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Prestamos prestamos = db.Prestamos.Find(id);
        //    if (prestamos == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(prestamos);
        //}

        //// POST: Prestamos/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prestamos prestamos = db.Prestamos.Find(id);
            db.Prestamos.Remove(prestamos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Metodo para guardar libros en la tabla temporal
        public JsonResult GuardarLibro(int LibroID)
        {
            var resultado = true;

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    var libros = (from a in db.Libros where a.LibroID == LibroID select a).SingleOrDefault();
                    libros.EstadoLibro = EstadoLibro.Prestado;
                    db.SaveChanges();

                     var libroGuardar = new PrestamosDetallesT
                    {
                        LibroID = libros.LibroID,
                        LibroTitulo = libros.LibroTitulo
                    };
                    db.PrestamosDetallesT.Add(libroGuardar);
                    db.SaveChanges();

                    transaccion.Commit();

                    resultado = true;
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    resultado = false; 
                    
                }
            }

            var librosCombo = (from a in db.Libros where a.EstadoLibro == EstadoLibro.Disponible select a).ToList();
            ViewBag.LibroID = new SelectList(librosCombo, "LibroID", "LibroTitulo");

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        //Metodo para mostrar libros en tabla temporal.
        public JsonResult BuscarLibros()
        {
            List<PrestamosDetallesT> listadoPrestamosDetallesT = new List<PrestamosDetallesT>();

            var librosTemporal = (from a in db.PrestamosDetallesT select a).ToList();

            foreach (var item in librosTemporal)
            {
                var libroBuscar = new PrestamosDetallesT
                {
                    PrestamoDetalleTID = item.PrestamoDetalleTID,
                    LibroID = item.LibroID,
                    LibroTitulo = item.LibroTitulo
                };
                listadoPrestamosDetallesT.Add(libroBuscar);
            }


            return Json(listadoPrestamosDetallesT, JsonRequestBehavior.AllowGet);
        }

        //Metodo para cancelar prestamo
        public JsonResult CancelarPrestamo()
        {
            var resultado = true;

            var prestamosDetallesT = (from a in db.PrestamosDetallesT select a).ToList();

            using (var transacion = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in prestamosDetallesT)
                    {
                        var libro = (from a in db.Libros where a.LibroID == item.LibroID select a).SingleOrDefault();
                        libro.EstadoLibro = EstadoLibro.Disponible;
                    }

                    db.PrestamosDetallesT.RemoveRange(prestamosDetallesT);
                    db.SaveChanges();

                    transacion.Commit();


                    resultado = true;
                }
                catch (Exception ex)
                {
                    transacion.Rollback();
                    resultado = false;

                }
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        ////Metodo para eliminar un libro de la tabla temporal
        public JsonResult QuitarLibro(int PrestamoDetalleTID)
        {
            var resultado = true;
            var prestamosDetalleT = (from a in db.PrestamosDetallesT select a).ToList();

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    PrestamosDetallesT prestamosDetallesT = db.PrestamosDetallesT.Find(PrestamoDetalleTID);
                    db.PrestamosDetallesT.Remove(prestamosDetallesT);

                    var libro = (from a in db.Libros where a.LibroID == prestamosDetallesT.LibroID select a).SingleOrDefault();
                    libro.EstadoLibro = EstadoLibro.Disponible;
                    db.SaveChanges();

                    transaccion.Commit();

                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    resultado = false;
                }
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
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
