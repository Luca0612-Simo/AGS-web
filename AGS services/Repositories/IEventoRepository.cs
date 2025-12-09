using AGS_Models;

namespace AGS_services.Repositories
{
    public interface IEventoRepository
    {
        Task<IEnumerable<Evento>> GetAllEventos();
        Task<Evento> GetByIdEvento(int id);
        Task<Evento> CreateEvento(Evento evento);
        Task UpdateEvento(Evento evento);
        Task DeleteEvento(int id);
    }
}