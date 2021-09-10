using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestFacturacionMVC.NETFramework.Models
{
    public class JoinDetalleProducto
    {
        public Detalle detalle { get; set; }
        public Producto producto { get; set; }
    }
}