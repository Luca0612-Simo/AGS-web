using AGS_Models;
using AGS_Models.DTO;

namespace AGS_services.Repositories
{
    public interface IEventoService
    {
        Task<IEnumerable<Evento>> GetAllEventos();
        Task<Evento> GetByIdEvento(int id);
        Task<Evento> CreateEvento(EventoCreateDTO dto);
        Task<UserResultDTO> UpdateEvento(int id, EventoUpdateDTO dto);
        Task<UserResultDTO> DeleteEvento(int id);
    }
}