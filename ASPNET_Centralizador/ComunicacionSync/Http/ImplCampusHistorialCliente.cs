using ASPNET_Centralizador.DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ASPNET_Centralizador.ComunicacionSync.Http
{
    

    public class ImplCampusHistorialCliente : ICampusHistorialCliente
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        public ImplCampusHistorialCliente(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }
        public async Task ComunicarseConCampus(EstudianteReadDTO est)
        {
            StringContent cuerpoHttp = new StringContent(
                JsonSerializer.Serialize(est), Encoding.UTF8, "application/json");
            var respuesta = await httpClient.PostAsync($"{configuration["CampusService"]}", cuerpoHttp);
            if (respuesta.IsSuccessStatusCode)
                Console.WriteLine("Envío de POST sincronizado hacia servico Campus tuvo éxito");
            else
                Console.WriteLine("Envío de POST sincronizado hacia servico Campus NO tuvo éxito");
        }
    }
}
