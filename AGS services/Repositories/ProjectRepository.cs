using AGS_models;
using AGS_Models;
using AGS_services.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGS_services
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Proyecto> GetByIdProject(int id)
        {
            return await _context.Proyectos.FindAsync(id);
        }

        public async Task<IEnumerable<Proyecto>> GetProjects()
        {
            return await _context.Proyectos.ToListAsync();
        }

        public async Task<Proyecto> AddProject(Proyecto proyecto)
        {
            await _context.Proyectos.AddAsync(proyecto);
            await _context.SaveChangesAsync();
            return proyecto;
        }

        public async Task UpdateProject(Proyecto proyecto)
        {
            _context.Proyectos.Update(proyecto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProject(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto != null)
            {
                _context.Proyectos.Remove(proyecto);
                await _context.SaveChangesAsync();
            }
        }
    }
}