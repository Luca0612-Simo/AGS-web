using AGS_Models;
using AGS_Models.DTO;
using AGS_services.Repositories;

namespace AGS_services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _repo;

        public EventoService(IEventoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Evento>> GetAllEventos()
        {
            return await _repo.GetAllEventos();
        }

        public async Task<Evento> GetByIdEvento(int id)
        {
            return await _repo.GetByIdEvento(id);
        }

        public async Task<Evento> CreateEvento(EventoCreateDTO dto)
        {
            var evento = new Evento
            {
                nombre = dto.nombre,
                horas = dto.horas,
                fecha = dto.fecha
            };
            return await _repo.CreateEvento(evento);
        }

        public async Task<UserResultDTO> UpdateEvento(int id, EventoUpdateDTO dto)
        {
            var result = new UserResultDTO { Result = false };
            var evento = await _repo.GetByIdEvento(id);

            if (evento == null)
            {
                result.Message = "Evento no encontrado";
                return result;
            }

            if (!string.IsNullOrEmpty(dto.nombre)) evento.nombre = dto.nombre;
            if (dto.horas != null) evento.horas = dto.horas.Value;

            if (dto.fecha != null) evento.fecha = dto.fecha.Value;

            await _repo.UpdateEvento(evento);

            result.Result = true;
            result.Message = "Evento actualizado";
            return result;
        }

        public async Task<UserResultDTO> DeleteEvento(int id)
        {
            var result = new UserResultDTO { Result = false };
            var evento = await _repo.GetByIdEvento(id);

            if (evento == null)
            {
                result.Message = "Evento no encontrado";
                return result;
            }

            await _repo.DeleteEvento(id);

            result.Result = true;
            result.Message = "Evento eliminado";
            return result;
        }
    }
}