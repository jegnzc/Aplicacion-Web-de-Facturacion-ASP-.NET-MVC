using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestFacturacionMVC.NETFramework.Models;

namespace TestFacturacionMVC.NETFramework.Controllers
{
    public class FacturacionController : Controller
    {
        private FacturaTestEntity db = new FacturaTestEntity();

        // GET: Facturacion
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Buscar(int id)
        {
            List<Detalle> detalles = db.Detalle.ToList();
            List<Producto> productos = db.Producto.ToList();
            List<Factura> facturas = db.Factura.ToList();
            List<Cliente> clientes = db.Cliente.ToList();

            dynamic model = new ExpandoObject();

            var viewCliente = (from f in facturas
                               join c in clientes
                               on f.fkIdCliente
                               equals c.pkIdCliente
                               into table1
                               from c in table1.DefaultIfEmpty()
                               where f.pkIdFactura == id
                               select new JoinClienteFactura { cliente = c, factura = f });
            model.cliente = viewCliente;

            var viewDetalles =
                           from d in detalles
                           join p in productos
                           on d.fkIdProducto
                           equals p.pkIdProducto
                           into table1
                           from p in table1.DefaultIfEmpty()
                           where d.fkIdFactura == id
                           select new JoinDetalleProducto { detalle = d, producto = p };
            model.detalles = viewDetalles;
            ViewData["detalles"] = viewDetalles;

            return View(model);
        }
    }
}