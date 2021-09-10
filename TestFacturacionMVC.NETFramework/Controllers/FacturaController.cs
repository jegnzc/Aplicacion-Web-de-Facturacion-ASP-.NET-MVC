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
    public class FacturaController : Controller
    {
        private FacturaTestEntity db = new FacturaTestEntity();

        public ActionResult Index()
        {
            var factura = db.Factura.Include(f => f.Cliente);
            return View(factura.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        public ActionResult Create()
        {
            ViewBag.fkIdCliente = new SelectList(db.Cliente, "pkIdCliente", "nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pkIdFactura,fecha,fkIdCliente")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Factura.Add(factura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fkIdCliente = new SelectList(db.Cliente, "pkIdCliente", "nombre", factura.fkIdCliente);
            return View(factura);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.fkIdCliente = new SelectList(db.Cliente, "pkIdCliente", "nombre", factura.fkIdCliente);
            return View(factura);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pkIdFactura,fecha,fkIdCliente")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fkIdCliente = new SelectList(db.Cliente, "pkIdCliente", "nombre", factura.fkIdCliente);
            return View(factura);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factura factura = db.Factura.Find(id);
            db.Factura.Remove(factura);
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