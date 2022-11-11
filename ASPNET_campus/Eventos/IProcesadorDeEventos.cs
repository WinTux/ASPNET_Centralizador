namespace ASPNET_campus.Eventos
{
    public interface IProcesadorDeEventos
    {
        void ProcesarEvento(string tipo);
    }
}
