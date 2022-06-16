using Newtonsoft.Json;
using PruebaCrud.Models.DTO;
using RestSharp;

namespace PruebaCrud.Servicios
{
    public interface IClienteCrud
    {
        Task<EditClienteDTO> BuscarCliente(string cliente);
        Task CrearCliente(ClienteDTO cliente);
        Task EditarCliente(EditClienteDTO cliente);
        Task EliminarCliente(string cliente);
        Task<ClienteDTO> PrepararForm();
        Task<List<ClienteRes>> PrepararListaClientes();
    }

    public class ClienteCrud : IClienteCrud
    {
        public async Task<ClienteDTO> PrepararForm()
        {

            var client = new RestClient("https://localhost:7071/api/v1/Consultar/EstadosyTiposIde");
            var request = new RestRequest("", Method.Get);
            var response = await client.ExecuteAsync(request);

            var respu = JsonConvert.DeserializeObject<ClienteDTO>(response.Content);

            return respu;

        }

        public async Task<List<ClienteRes>> PrepararListaClientes()
        {

            var client = new RestClient("https://localhost:7071/api/v1/Consultar/Clientes");
            var request = new RestRequest("", Method.Get);
            var response = await client.ExecuteAsync(request);

            var respu = JsonConvert.DeserializeObject<List<ClienteRes>>(response.Content);

            return respu;

        }

        public async Task CrearCliente(ClienteDTO cliente)
        {
            JsonCliente jsonCliente = new JsonCliente();

            jsonCliente.Docu_cli = cliente.Docu_cli;
            jsonCliente.Tip_ide = cliente.Tip_ide;
            jsonCliente.Nombres = cliente.Nombres;
            jsonCliente.Apellidos = cliente.Apellidos;
            jsonCliente.Est_cli = cliente.Est_cli;

            var client = new RestClient("https://localhost:7071/api/v1/CrearCliente");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(jsonCliente);
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            await client.ExecuteAsync(request);

        }

        public async Task EditarCliente(EditClienteDTO cliente)
        {

            var client = new RestClient("https://localhost:7071/api/v1/Editar/Cliente");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(cliente);
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            await client.ExecuteAsync(request);

        }

        public async Task<EditClienteDTO> BuscarCliente(string cliente)
        {

            var client = new RestClient("https://localhost:7071/api/v1/Consultar/Cliente?docu_id=" + cliente);
            var request = new RestRequest("", Method.Get);

            var response = await client.ExecuteAsync<EditClienteDTO>(request);

            var respu = JsonConvert.DeserializeObject<EditClienteDTO>(response.Content);

            return respu;

        }

        public async Task EliminarCliente(string cliente)
        {

            var client = new RestClient("https://localhost:7071/api/v1/Eliminar/Cliente?docu_id=" + cliente);
            var request = new RestRequest("", Method.Post);

            await client.ExecuteAsync(request);


        }


    }
}
