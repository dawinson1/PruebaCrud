using ApiPruebaCrud.Servicios;
using ApiPruebaCrud.Utilidades;
using Microsoft.OpenApi.Models;

namespace ApiPruebaCrud
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opciones =>
                opciones.Conventions.Add(new SwaggerPorVersion())
            );

            services.AddTransient<IRepositorioCliente, RepositorioCliente>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Crud", Version = "v1" })
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Crud v1"); }

                );
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                endpoints.MapControllers()
            );
        }


    }
}
