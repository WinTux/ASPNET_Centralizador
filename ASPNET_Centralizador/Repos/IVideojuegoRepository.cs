using ASPNET_Centralizador.Models;
using System.Collections.Generic;

namespace ASPNET_Centralizador.Repos
{
    public interface IVideojuegoRepository
    {
        IEnumerable<Videojuego> GetVideojuegos();
        Videojuego GetVideojuego(int id);
    }
}
