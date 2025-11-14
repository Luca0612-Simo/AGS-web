using AGS_Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGS_services.Repositories
{
    public interface IProjectRepository
    {
        Task<Proyecto> GetByIdProject(int id);
        Task<IEnumerable<Proyecto>> GetProjects();
        Task<Proyecto> AddProject(Proyecto proyecto);
        Task UpdateProject(Proyecto proyecto);
        Task DeleteProject(int id);
    }
}