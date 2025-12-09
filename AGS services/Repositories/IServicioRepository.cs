using AGS_Models;

namespace AGS_services.Repositories
{
    public interface IServicioRepository
    {
        Task<Servicio> GetByIdServicio(int id);
        Task<IEnumerable<Servicio>> GetServicios();
        Task<Servicio> AddServicio(Servicio servicio);
        Task UpdateServicio(Servicio servicio);
        Task DeleteServicio(int id);
    }
}