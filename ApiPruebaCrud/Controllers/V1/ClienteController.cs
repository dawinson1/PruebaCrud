using ApiPruebaCrud.Models.DTO;
using ApiPruebaCrud.Models.Respuestas;
using ApiPruebaCrud.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace ApiPruebaCrud.Controllers.V1
{
    [ApiController]
    [Route("api/v1")]
    public class ClienteController : ControllerBase
    {
        private readonly IRepositorioCliente repositorioCliente;

        public ClienteController(IRepositorioCliente repositorioCliente)
        {
            this.repositorioCliente = repositorioCliente;
        }

        [HttpGet]
        [Route("Consultar/EstadosyTiposIde")]
        public async Task<ActionResult<EstadosyTiposIde>> ConsultarPersonas()
        {
            return await repositorioCliente.Consultar_EstadosyTipos();
        }

        [HttpGet]
        [Route("Consultar/Clientes")]
        public async Task<ActionResult<List<ClienteRes>>> ConsultarClientes()
        {
            var Respu = await repositorioCliente.ConsultarClientes();

            return Respu;
        }

        [HttpGet]
        [Route("Consultar/Cliente")]
        public async Task<ActionResult<EditCliente>> ConsultarCliente([FromQuery] string docu_id)
        {
            var Respu = await repositorioCliente.ConsultarCliente(docu_id);

            return Respu;
        }

        [HttpPost]
        [Route("Editar/Cliente")]
        public async Task<ActionResult> EditarCliente([FromBody] EditCliente editCliente)
        {
            await repositorioCliente.EditarCliente(editCliente);

            return Ok();

        }

        [HttpPost]
        [Route("CrearCliente")]
        public async Task<ActionResult> ConsultarPersonas([FromBody] ClienteDTO cliente)
        {
            await repositorioCliente.CrearCliente(cliente);

            return Ok();
        }

        [HttpPost]
        [Route("Eliminar/Cliente")]
        public async Task<ActionResult> EliminarPersonas([FromQuery] string docu_id)
        {
            await repositorioCliente.BorrarCliente(docu_id);

            return Ok();
        }


    }
}
