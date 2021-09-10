using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestFacturacionMVC.NETFramework.Models;

namespace TestFacturacionMVC.NETFramework.Controllers
{
    public class DetalleController : Controller
    {
        private FacturaTestEntity db = new FacturaTestEntity();

        public ActionResult Index()
        {
            var detalle = db.Detalle.Include(d => d.Factura).Include(d => d.Producto);
            return View(detalle.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalle.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            return View(detalle);
        }

        public ActionResult Create()
        {
            ViewBag.fkIdFactura = new SelectList(db.Factura, "pkIdFactura", "pkIdFactura");
            ViewBag.fkIdProducto = new SelectList(db.Producto, "pkIdProducto", "nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pkIdDetalle,fkIdProducto,fkIdFactura,cantidad")] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                db.Detalle.Add(detalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fkIdFactura = new SelectList(db.Factura, "pkIdFactura", "pkIdFactura", detalle.fkIdFactura);
            ViewBag.fkIdProducto = new SelectList(db.Producto, "pkIdProducto", "nombre", detalle.fkIdProducto);
            return View(detalle);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalle.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.fkIdFactura = new SelectList(db.Factura, "pkIdFactura", "pkIdFactura", detalle.fkIdFactura);
            ViewBag.fkIdProducto = new SelectList(db.Producto, "pkIdProducto", "nombre", detalle.fkIdProducto);
            return View(detalle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pkIdDetalle,fkIdProducto,fkIdFactura,cantidad")] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fkIdFactura = new SelectList(db.Factura, "pkIdFactura", "pkIdFactura", detalle.fkIdFactura);
            ViewBag.fkIdProducto = new SelectList(db.Producto, "pkIdProducto", "nombre", detalle.fkIdProducto);
            return View(detalle);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalle.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            return View(detalle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Detalle detalle = db.Detalle.Find(id);
            db.Detalle.Remove(detalle);
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