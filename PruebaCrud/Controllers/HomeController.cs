using Microsoft.AspNetCore.Mvc;
using PruebaCrud.Models;
using PruebaCrud.Models.DTO;
using PruebaCrud.Servicios;
using System.Diagnostics;

namespace PruebaCrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IClienteCrud ClienteCrud { get; }

        public HomeController(ILogger<HomeController> logger, IClienteCrud clienteCrud)
        {
            _logger = logger;
            ClienteCrud = clienteCrud;
        }

        public async Task<IActionResult> Index()
        {
            var datos = await ClienteCrud.PrepararListaClientes();

            return View(datos);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {

            var datos = await ClienteCrud.PrepararForm();

            return View(datos);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ClienteDTO cliente)
        {
            if(!ModelState.IsValid)
            {
                return View(cliente);
            }

            await ClienteCrud.CrearCliente(cliente);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(string Docu_cli)
        {

            var datos = await ClienteCrud.BuscarCliente(Docu_cli);

            return View(datos);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditClienteDTO edit)
        {

            await ClienteCrud.EditarCliente(edit);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Eliminar(string Docu_cli)
        {

            await ClienteCrud.EliminarCliente(Docu_cli);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}