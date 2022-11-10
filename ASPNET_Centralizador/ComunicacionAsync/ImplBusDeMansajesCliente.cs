using ASPNET_Centralizador.DTO;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using IModel = RabbitMQ.Client.IModel;

namespace ASPNET_Centralizador.ComunicacionAsync
{
    public class ImplBusDeMansajesCliente : IBusDeMansajesCliente
    {
        private readonly IConfiguration configuration;
        private readonly IConnection conexion;
        private readonly IModel canal;

        public ImplBusDeMansajesCliente(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionFactory factory = new ConnectionFactory() {
                HostName = configuration["Host_RabbitMQ"],
                Port = int.Parse(configuration["Puerto_RabbitMQ"])
            };
            try { 
                conexion = factory.CreateConnection();
                canal = conexion.CreateModel();
                canal.ExchangeDeclare(
                    exchange: "mi_exchange",
                    type: ExchangeType.Fanout
                );
                //opcional: agegar un comportamiento personalizado
                conexion.ConnectionShutdown += RabbitMQ_evento_shutdown;
            }
            catch (Exception e) { }
        }
        //opcional
        public void RabbitMQ_evento_shutdown(object sender,ShutdownEventArgs args) { 
            //Acá uno pone lo que necesite
        }

        public void PublicarNuevoEstudiante(EstudiantePublicadoDTO estudiantePublicadoDTO)
        {
            string mensaje = JsonSerializer.Serialize(estudiantePublicadoDTO);
            if (conexion.IsOpen)
                Enviar(mensaje);
            else
                Console.WriteLine("No se pudo enviar el mensaje al bus de mensajes RabbitMQ");
        }

        private void Enviar(string mensaje)
        {
            var cuerpo = Encoding.UTF8.GetBytes(mensaje);
            canal.BasicPublish(
                exchange: "mi_exchange",
                routingKey: "",
                basicProperties: null,
                body: cuerpo
            );
            Console.WriteLine("Se envió el mensaje al bus de mensajes RabbitMQ");
        }

        private void Finalizar() {
            if (canal.IsOpen) {
                canal.Close();
                conexion.Close();
            }
        }
    }
}
