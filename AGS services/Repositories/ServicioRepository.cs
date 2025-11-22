using AGS_Models;
using Microsoft.EntityFrameworkCore;

namespace AGS_services.Repositories
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly ApplicationDbContext _context;

        public ServicioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Servicio> GetByIdServicio(int id) => await _context.Servicios.FindAsync(id);
        public async Task<IEnumerable<Servicio>> GetServicios() => await _context.Servicios.ToListAsync();

        public async Task<Servicio> AddServicio(Servicio servicio)
        {
            await _context.Servicios.AddAsync(servicio);
            await _context.SaveChangesAsync();
            return servicio;
        }

        public async Task UpdateServicio(Servicio servicio)
        {
            _context.Servicios.Update(servicio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio != null)
            {
                _context.Servicios.Remove(servicio);
                await _context.SaveChangesAsync();
            }
        }
    }
}