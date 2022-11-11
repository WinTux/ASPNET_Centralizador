using ASPNET_campus.Eventos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASPNET_campus.ComunicacionAsync
{
    public class BusDeMensajesSuscriptor : BackgroundService
    {
        private readonly IConfiguration configuration;
        private readonly IProcesadorDeEventos procesador;

        private IConnection conexion;
        private IModel canal;
        private string cola;
        public BusDeMensajesSuscriptor(IConfiguration configuration, IProcesadorDeEventos procesador)
        {
            this.configuration = configuration;
            this.procesador = procesador;
            IniciarRabbitMQ();
        }

        private void IniciarRabbitMQ()
        {
            var factory = new ConnectionFactory() { 
                HostName = configuration["Host_RabbitMQ"],
                Port = int.Parse(configuration["Puerto_RabbitMQ"])
            };
            conexion = factory.CreateConnection();
            canal = conexion.CreateModel();
            canal.ExchangeDeclare(
                exchange: "mi_exchange",
                type: ExchangeType.Fanout
            );
            cola = canal.QueueDeclare().QueueName;
            canal.QueueBind(
                queue: cola,
                exchange: "mi_exchange",
                routingKey: ""
            );
            //Opcional
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();//detener si se lo solicita
            var consumidor = new EventingBasicConsumer(canal);//Establece un nuevo consumidor RabbitMQ
            consumidor.Received += (modulo, eveArgs) => {
                Console.WriteLine("Un evento ocurrió");
                var cuerpo = eveArgs.Body;
                var mensaje = Encoding.UTF8.GetString(cuerpo.ToArray());
                procesador.ProcesarEvento(mensaje);
            };
            canal.BasicConsume(
                queue: cola,
                autoAck: true,
                consumer: consumidor
            );
            return Task.CompletedTask;
        }
    }
}
