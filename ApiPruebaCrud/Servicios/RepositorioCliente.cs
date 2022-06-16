using ApiPruebaCrud.Models.DTO;
using ApiPruebaCrud.Models.Respuestas;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiPruebaCrud.Servicios
{
    public interface IRepositorioCliente
    {
        Task BorrarCliente(string docu_cli);
        Task<EditCliente> ConsultarCliente(string docu_id);
        Task<List<ClienteRes>> ConsultarClientes();
        Task<EstadosyTiposIde> Consultar_EstadosyTipos();
        Task CrearCliente(ClienteDTO cliente);
        Task EditarCliente(EditCliente editCliente);
    }

    public class RepositorioCliente : IRepositorioCliente
    {
        private readonly string configuration;

        public RepositorioCliente(IConfiguration configuration)
        {
            this.configuration = configuration.GetConnectionString("defaultConnection");
        }

        public async Task<EstadosyTiposIde> Consultar_EstadosyTipos()
        {
            EstadosyTiposIde estadosyTiposIde = new EstadosyTiposIde();

            string query1 = @"Select * From Tipo_Identificacion;";
            string query2 = @"Select * From Estados_Cliente;";

            using var connection = new SqlConnection(configuration);
            var tipos = await connection.QueryAsync<Tipo_IdeDTO>(query1);
            var estados = await connection.QueryAsync<Estado_CliDTO>(query2);

            estadosyTiposIde.Tipos_Ide = tipos.ToList();
            estadosyTiposIde.Estados_Cli = estados.ToList();

            return estadosyTiposIde;

        }

        public async Task CrearCliente(ClienteDTO cliente)
        {

            string query = @"Insert Into Cliente (Docu_cli, Tip_ide, Nombres, Apellidos, Est_cli) 
                                            Values (@Docu_cli, @Tip_ide, @Nombres, @Apellidos, @Est_cli);";

            using var connection = new SqlConnection(configuration);
            await connection.QueryAsync(query, cliente);

        }

        public async Task<List<ClienteRes>> ConsultarClientes()
        {

            string query = @"Sp_ConsultarClientes";

            using var connection = new SqlConnection(configuration);
            var personas = await connection.QueryAsync<ClienteRes>(query);

            return personas.ToList();


        }

        public async Task<EditCliente> ConsultarCliente(string docu_cli)
        {

            string query = @"Select Top(1) Docu_cli, Nombres, Apellidos From Cliente
                                            Where Docu_cli = @Docu_cli";

            using var connection = new SqlConnection(configuration);
            var DCliente = await connection.QueryAsync<EditCliente>(query, new { docu_cli});

            return DCliente.FirstOrDefault();


        }

        public async Task EditarCliente(EditCliente editCliente)
        {

            string query = @"Update Cliente Set Nombres = @Nombres, Apellidos = @Apellidos 
                                            Where Docu_cli = @Docu_cli";

            using var connection = new SqlConnection(configuration);
            await connection.QueryAsync<EditCliente>(query, new { editCliente.Nombres, editCliente.Apellidos, editCliente.Docu_cli });

        }

        public async Task BorrarCliente(string docu_cli)
        {

            string query = @"Delete Cliente Where Docu_cli = @Docu_cli";

            using var connection = new SqlConnection(configuration);
            await connection.ExecuteAsync(query, new { docu_cli });


        }


    }
}
