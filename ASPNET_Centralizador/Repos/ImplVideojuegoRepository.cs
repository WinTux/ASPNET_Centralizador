using ASPNET_Centralizador.Models;
using System.Collections.Generic;

namespace ASPNET_Centralizador.Repos
{
    public class ImplVideojuegoRepository : IVideojuegoRepository
    {
        public Videojuego GetVideojuego(int id)
        {
            return new Videojuego {
                id = id,
                nombre = "Mario Bros.",
                plataforma = "Famicom",
                instrucciones = "Llegue hasta la meta"
            };
        }

        public IEnumerable<Videojuego> GetVideojuegos()
        {
            var vj = new List<Videojuego> {
                new Videojuego{
                    id = 1,
                nombre = "Mario Bros.",
                plataforma = "Famicom",
                instrucciones = "Llegue hasta la meta"
                },
                new Videojuego{
                    id = 2,
                nombre = "Leyend of Zelda",
                plataforma = "Famicom",
                instrucciones = "Sobrevive y rescata a la princesa"
                }
            };
            return vj;
        }
    }
}
