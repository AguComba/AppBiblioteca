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
    public class DevolucionesController : Controller
    {
        private AppBibliotecaContext db = new AppBibliotecaContext();

        // GET: Devoluciones
        public ActionResult Index()
        {
            var devoluciones = db.Devoluciones.Include(d => d.Socios);
            return View(devoluciones.ToList());
        }

        // GET: Devoluciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devoluciones devoluciones = db.Devoluciones.Find(id);
            if (devoluciones == null)
            {
                return HttpNotFound();
            }
            return View(devoluciones);
        }

        // GET: Devoluciones/Create
        public ActionResult Create()
        {
            ViewBag.SocioID = new SelectList(db.Socios, "SocioID", "SocioNombreCompleto");
            
            var librosCombo = (from a in db.Libros where a.EstadoLibro == EstadoLibro.Prestado select a).ToList();
            ViewBag.LibroID = new SelectList(librosCombo, "LibroID", "LibroTitulo");

            return View();
        }

        // POST: Devoluciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DevolucionID,DevolucionFecha,SocioID")] Devoluciones devoluciones)
        {
            if (ModelState.IsValid)
            {
                using (var transaccion = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Devoluciones.Add(devoluciones);
                        db.SaveChanges();

                        var devolucionesDetallesT = (from a in db.DevolucionesDetallesT select a).ToList();
                        foreach (var item in devolucionesDetallesT)
                        {
                            var libroGuardar = new DevolucionesDetalles
                            {
                                LibroID = item.LibroID,
                                DevolucionID = devoluciones.DevolucionID,
                            };
                            db.DevolucionesDetalles.Add(libroGuardar);
                            db.SaveChanges();
                        }
                        
                        db.DevolucionesDetallesT.RemoveRange(devolucionesDetallesT);
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

            ViewBag.SocioID = new SelectList(db.Socios, "SocioID", "SocioNombreCompleto", devoluciones.SocioID);

            var librosCombo = (from a in db.Libros where a.EstadoLibro == EstadoLibro.Prestado select a).ToList();
            ViewBag.LibroID = new SelectList(librosCombo, "LibroID", "LibroTitulo");

            return View(devoluciones);
        }

        // GET: Devoluciones/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Devoluciones devoluciones = db.Devoluciones.Find(id);
        //    if (devoluciones == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.SocioID = new SelectList(db.Socios, "SocioID", "SocioNombreCompleto", devoluciones.SocioID);
        //    return View(devoluciones);
        //}

        // POST: Devoluciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "DevolucionID,DevolucionFecha,SocioID")] Devoluciones devoluciones)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(devoluciones).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.SocioID = new SelectList(db.Socios, "SocioID", "SocioNombreCompleto", devoluciones.SocioID);
        //    return View(devoluciones);
        //}

        // GET: Devoluciones/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Devoluciones devoluciones = db.Devoluciones.Find(id);
        //    if (devoluciones == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(devoluciones);
        //}

       /// POST: Devoluciones/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Devoluciones devoluciones = db.Devoluciones.Find(id);
            db.Devoluciones.Remove(devoluciones);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Metodo GuardarLibroD

        public JsonResult GuardarLibroD(int LibroID)
        {
            var resultado = true;

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    var libros = (from a in db.Libros where a.LibroID == LibroID select a).SingleOrDefault();
                    libros.EstadoLibro = EstadoLibro.Disponible;
                    db.SaveChanges();

                    var libroGuardarD = new DevolucionesDetallesT
                    {
                        LibroID = libros.LibroID,
                        LibroTitulo = libros.LibroTitulo
                    };
                    db.DevolucionesDetallesT.Add(libroGuardarD);
                    db.SaveChanges();

                    transaccion.Commit();
                }
                catch (Exception ex)
                {

                    transaccion.Rollback();
                    resultado = false;
                }
            }

            var librosCombo = (from a in db.Libros where a.EstadoLibro == EstadoLibro.Prestado select a).ToList();
            ViewBag.LibroID = new SelectList(librosCombo, "LibroID", "LibroTitulo");
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        //Metodo BuscarLibroD

        public JsonResult BuscarLibroD()
        {
            List<DevolucionesDetallesT> listadoDevolucionesDetallesT = new List <DevolucionesDetallesT>();

            var librosTemporal = (from a in db.DevolucionesDetallesT select a).ToList();
            foreach (var item in librosTemporal)
            {
                var libroBuscar = new DevolucionesDetallesT
                {
                    DevolucionesDetallesTID = item.DevolucionesDetallesTID,
                    LibroID = item.LibroID,
                    LibroTitulo = item.LibroTitulo
                };
                listadoDevolucionesDetallesT.Add(libroBuscar);
            };

            return Json(listadoDevolucionesDetallesT, JsonRequestBehavior.AllowGet);
        }

        //Cancelar devolucion

       public JsonResult CancelarDevolucion()
        {
            var resultado = true;

            var devolucionesDetalleT = (from a in db.DevolucionesDetallesT select a).ToList();

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    foreach(var item in devolucionesDetalleT)
                    {
                        var libro = (from a in db.Libros where a.LibroID == item.LibroID select a).SingleOrDefault();
                        libro.EstadoLibro = EstadoLibro.Prestado;
                        db.SaveChanges();
                    }

                    db.DevolucionesDetallesT.RemoveRange(devolucionesDetalleT);
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
            return Json(resultado, JsonRequestBehavior.AllowGet);
        } 

        public JsonResult EliminarLibroD( int DevolucionesDetallesTID)
        {
            var resultado = true;

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    DevolucionesDetallesT devolucionesDetallesT = db.DevolucionesDetallesT.Find(DevolucionesDetallesTID);
                    db.DevolucionesDetallesT.Remove(devolucionesDetallesT);

                    var libro = (from a in db.Libros where a.LibroID == devolucionesDetallesT.LibroID select a).SingleOrDefault();
                    libro.EstadoLibro = EstadoLibro.Prestado;
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
