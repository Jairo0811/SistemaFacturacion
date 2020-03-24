using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Facturacion.Data;
using Sistema.Facturacion.Models;

namespace Sistema.Facturacion.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public HomeController(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;

            if (!_context.Users.Any())
            {
                AgregarUsuarios().Wait();
            }
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            var model = new DashboardViewModel()
            {
                TotalClientes = _context.Clientes.Count(),
                TotalProductos = _context.Productos.Count(),
                TotalFacturas = _context.Facturas.Count(),
                TotalUsuarios = _context.Users.Count()
            };
            return View(model);
        }

        public IActionResult Clientes()
        {
            var model = _context.Clientes
                .Include(m => m.Usuario)
                .ToList();
            return View(model);
        }

        public IActionResult CrearCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearCliente(Cliente cliente)
        {
            cliente.UsuarioId = _userManager.GetUserId(User);
            cliente.Fecha = DateTime.Now;
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return RedirectToAction("Clientes");
        }

        public IActionResult Productos()
        {
            var productos = _context.Productos
                .Include(m => m.Usuario)
                .ToList();
            return View(productos);
        }

        public IActionResult CrearProducto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearProducto(Producto producto)
        {
            producto.UsuarioId = _userManager.GetUserId(User);
            producto.Fecha = DateTime.Now;
            _context.Productos.Add(producto);
            _context.SaveChanges();
            return RedirectToAction("Productos");
        }

        public IActionResult Facturas()
        {
            var facturas = _context.Facturas
                .Include(m => m.Cliente)
                .Include(m => m.Producto)
                .Include(m => m.Usuario)
                .ToList();
            return View(facturas);
        }

        public IActionResult CrearFactura()
        {
            var model = new CrearFacturaViewModel()
            {
                Clientes = _context.Clientes.ToList(),
                Productos = _context.Productos.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CrearFactura(CrearFacturaViewModel model)
        {
            model.Factura.UsuarioId = _userManager.GetUserId(User);
            model.Factura.Fecha = DateTime.Now;
            _context.Facturas.Add(model.Factura);
            _context.SaveChanges();
            return RedirectToAction("Facturas");
        }

        public IActionResult Usuarios()
        {
            var usuarios = _context.Users.ToList();
            return View(usuarios);
        }

        public async Task AgregarUsuarios()
        {
            var DefaultUser1 = new ApplicationUser { UserName = "jairo@itla.com", Email = "jairo@itla.com" };
            await _userManager.CreateAsync(DefaultUser1, "Itla123.");

            var DefaultUser2 = new ApplicationUser { UserName = "diana@itla.com", Email = "Diana@itla.com" };
            await _userManager.CreateAsync(DefaultUser2, "Itla123.");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
