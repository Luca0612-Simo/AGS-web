using AGS_Models;
using AGS_Models.DTO;

namespace AGS_services.Repositories
{
    public interface IServicioService
    {
        Task<Servicio> GetByIdServicio(int id);
        Task<IEnumerable<Servicio>> GetServicios();
        Task<Servicio> AddServicio(ServicioCreateDTO dto);
        Task<UserResultDTO> UpdateServicio(int id, ServicioUpdateDTO dto);
        Task<UserResultDTO> DeleteServicio(int id);
    }
}