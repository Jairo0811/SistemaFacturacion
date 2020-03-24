using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Facturacion.Models
{
    public class Factura
    {
        [Key]
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public int ProductoId { get; set; }
        public string UsuarioId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }
        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }
        [ForeignKey("UsuarioId")]
        public ApplicationUser Usuario { get; set; }
    }
}
