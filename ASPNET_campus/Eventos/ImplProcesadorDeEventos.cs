using ASPNET_campus.Conexion;
using ASPNET_campus.DTO;
using ASPNET_campus.Models;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;

namespace ASPNET_campus.Eventos
{
    enum TipoDeEvento{
        estudiante_publicado,
        desconocido
    }
    public class ImplProcesadorDeEventos : IProcesadorDeEventos
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly IMapper mapper;
        public ImplProcesadorDeEventos(IServiceScopeFactory serviceScopeFactory, IMapper mapper) {
            this.serviceScopeFactory = serviceScopeFactory;
            this.mapper = mapper;
        }
        public void ProcesarEvento(string mensaje)
        {
            var tipo = DeterminarEvento(mensaje);
            switch (tipo)
            {
                case TipoDeEvento.estudiante_publicado:
                    agregarEstudiante(mensaje);
                    break;
                case TipoDeEvento.desconocido:
                    break;

            }

        }
        private TipoDeEvento DeterminarEvento(string mensaje) {
            EventoDTO tipo = JsonSerializer.Deserialize<EventoDTO>(mensaje);
            switch (tipo.evento) {
                case "estudiante_publicado":
                    return TipoDeEvento.estudiante_publicado;
                default:
                    return TipoDeEvento.desconocido;
            }
        }
        private void agregarEstudiante(string mensajeEstudiantePublicado) {
            using (var scope = serviceScopeFactory.CreateScope()) {
                var repo = scope.ServiceProvider.GetRequiredService<IPerfilRepository>();
                var estudiantePublicadoDTO = JsonSerializer.Deserialize<EstudiantePublicadoDTO>(mensajeEstudiantePublicado);
                try {
                    var est = mapper.Map<Estudiante>(estudiantePublicadoDTO);
                    if (!repo.ExisteEstudianteForaneo(est.fci)) {
                        repo.CrearEstudiante(est);
                        repo.Guardar();
                    }
                } catch (Exception e) {
                    Console.WriteLine($"Error al agregar un estudiante a la DDBB local: {e.Message}");
                }
            }
        }
    }
}
