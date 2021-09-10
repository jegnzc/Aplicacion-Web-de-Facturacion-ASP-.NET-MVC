using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestFacturacionMVC.NETFramework.Models
{
    public class JoinClienteFactura
    {
        public Cliente cliente { get; set; }
        public Factura factura { get; set; }
    }
}