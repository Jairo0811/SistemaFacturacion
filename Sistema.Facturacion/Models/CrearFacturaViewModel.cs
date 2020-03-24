using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Facturacion.Models
{
    public class CrearFacturaViewModel
    {
        public List<Cliente> Clientes { get; set; }
        public List<Producto> Productos { get; set; }
        public Factura Factura { get; set; }
    }
}
