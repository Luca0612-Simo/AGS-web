using AGS_models;
using AGS_Models;
using AGS_Models.DTO;
using AGS_services.Repositories;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGS_services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IFileStorageService _fileStorageService;

        public ProjectService(IProjectRepository projectRepository, IFileStorageService fileStorageService)
        {
            _projectRepository = projectRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<IEnumerable<Proyecto>> GetProjects()
        {
            var proyectos = await _projectRepository.GetProjects();
            foreach (var proyecto in proyectos)
            {
                proyecto.Url = _fileStorageService.GetFileUrl(proyecto.imagen);
            }
            return proyectos;
        }

        public async Task<Proyecto> GetByIdProject(int id)
        {
            var proyecto = await _projectRepository.GetByIdProject(id);
            if (proyecto != null)
            {
                proyecto.Url = _fileStorageService.GetFileUrl(proyecto.imagen);
            }
            return proyecto;
        }

        public async Task<Proyecto> CreateProject(ProjectCreateDTO projectDto)
        {
            string imageKey = await _fileStorageService.UploadFileAsync(projectDto.imagenFile);

            var proyecto = new Proyecto
            {
                nombre = projectDto.nombre,
                descripcion = projectDto.descripcion,
                imagen = imageKey,
                fecha_inicio = projectDto.fecha_inicio,
                estado = projectDto.estado,
                horas = projectDto.horas
            };

            return await _projectRepository.AddProject(proyecto);
        }

        public async Task<UserResultDTO> UpdateProject(int id, ProjectUpdateDTO projectDto)
        {
            var user_result = new UserResultDTO { Result = false };
            var proyectoFromDb = await _projectRepository.GetByIdProject(id);

            if (proyectoFromDb == null)
            {
                user_result.Message = "Proyecto no encontrado";
                return user_result;
            }

            if (!string.IsNullOrEmpty(projectDto.nombre))
            {
                proyectoFromDb.nombre = projectDto.nombre;
            }
            if (!string.IsNullOrEmpty(projectDto.descripcion))
            {
                proyectoFromDb.descripcion = projectDto.descripcion;
            }
            if (projectDto.fecha_inicio != null)
            {
                proyectoFromDb.fecha_inicio = projectDto.fecha_inicio;
            }
            if (projectDto.fecha_fin != null)
            {
                proyectoFromDb.fecha_fin = projectDto.fecha_fin;
            }
            if (!string.IsNullOrEmpty(projectDto.estado))
            {
                proyectoFromDb.estado = projectDto.estado;
            }
            if (projectDto.horas != null)
            {
                proyectoFromDb.horas = projectDto.horas.Value;
            }

            await _projectRepository.UpdateProject(proyectoFromDb);
            user_result.Result = true;
            user_result.Message = "Proyecto actualizado correctamente";
            return user_result;
        }

        public async Task<UserResultDTO> DeleteProject(int id)
        {
            var user_result = new UserResultDTO { Result = false };
            var proyectoFromDb = await _projectRepository.GetByIdProject(id);

            if (proyectoFromDb == null)
            {
                user_result.Message = "Proyecto no encontrado";
                return user_result;
            }

            proyectoFromDb.estado = "Finalizado";
            proyectoFromDb.fecha_fin = DateOnly.FromDateTime(DateTime.Now);
            await _projectRepository.UpdateProject(proyectoFromDb);

            user_result.Result = true;
            user_result.Message = "Proyecto finalizado";
            return user_result;
        }
    }
}