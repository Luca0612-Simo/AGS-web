using AGS_Models;
using Microsoft.EntityFrameworkCore;

namespace AGS_services.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly ApplicationDbContext _context;
        public EventoRepository(ApplicationDbContext context) { _context = context; }

        public async Task<IEnumerable<Evento>> GetAllEventos() => await _context.Eventos.ToListAsync();
        public async Task<Evento> GetByIdEvento(int id) => await _context.Eventos.FindAsync(id);

        public async Task<Evento> CreateEvento(Evento evento)
        {
            await _context.Eventos.AddAsync(evento);
            await _context.SaveChangesAsync();
            return evento;
        }
        public async Task UpdateEvento(Evento evento)
        {
            _context.Eventos.Update(evento);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteEvento(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null) { _context.Eventos.Remove(evento); await _context.SaveChangesAsync(); }
        }
    }
}