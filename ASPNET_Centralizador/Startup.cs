using ASPNET_Centralizador.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using ASPNET_Centralizador.ComunicacionSync.Http;
using ASPNET_Centralizador.ComunicacionAsync;

namespace ASPNET_Centralizador
{
    public class Startup
    {
        private readonly IWebHostEnvironment entorno;
        public Startup(IConfiguration configuration, IWebHostEnvironment entorno)
        {
            Configuration = configuration;
            this.entorno = entorno;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Variables de entorno para DDBB con Docker
            var servidor = Configuration["DBservidor"] ?? "192.168.1.253";
            var puerto = Configuration["DBpuerto"] ?? "1433";
            var usuario = Configuration["DBusuario"] ?? "ConexionParaAPI";
            var password = Configuration["DBpassword"] ?? "123456";

            services.AddControllers().AddNewtonsoftJson(
                s => s.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver());
            //IVideojuegoRepository -> ImplVideojuegoRepository
            services.AddScoped<IVideojuegoRepository, ImplVideojuegoRepository>();
            services.AddScoped<IEstudianteRepository, ImplEstudianteRepository>();

            //Selector de entorno
            if (entorno.IsProduction())
            {
                Console.WriteLine("Conectándome a la base de datos en producción");
                services.AddDbContext<InstitutoDbContext>(op => op.UseSqlServer(
                Configuration.GetConnectionString("InstitutoProd")));
            }
            else {
                Console.WriteLine("Conectándome a la base de datos en desarrollo");
                services.AddDbContext<InstitutoDbContext>(op => op.UseSqlServer(
                    Configuration.GetConnectionString("una_conexion")
                ));
            }
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddHttpClient<ICampusHistorialCliente,ImplCampusHistorialCliente>();

            services.AddSingleton<IBusDeMansajesCliente, ImplBusDeMansajesCliente>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
