using AGS_Models;
using AGS_Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGS_services.Repositories
{
    public interface IProjectService
    {
        Task<Proyecto> GetByIdProject(int id);
        Task<IEnumerable<Proyecto>> GetProjects();
        Task<Proyecto> CreateProject(ProjectCreateDTO proyectoDto);
        Task<UserResultDTO> UpdateProject(int id, ProjectUpdateDTO projectDto);
        Task<UserResultDTO> UpdateProjectHours(int id, int newHours);
        Task<UserResultDTO> DeleteProject(int id);
    }
}