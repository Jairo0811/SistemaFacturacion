using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Facturacion.Models
{
    public class DashboardViewModel
    {
        public int TotalClientes { get; set; }
        public int TotalProductos { get; set; }
        public int TotalFacturas { get; set; }
        public int TotalUsuarios { get; set; }
    }
}
