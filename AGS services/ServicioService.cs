using AGS_Models;
using AGS_Models.DTO;
using AGS_services.Repositories;

namespace AGS_services
{
    public class ServicioService : IServicioService
    {
        private readonly IServicioRepository _repository;
        private readonly IFileStorageService _fileStorage;

        public ServicioService(IServicioRepository repository, IFileStorageService fileStorage)
        {
            _repository = repository;
            _fileStorage = fileStorage;
        }

        public async Task<IEnumerable<Servicio>> GetServicios()
        {
            var servicios = await _repository.GetServicios();
            foreach (var s in servicios)
            {
                s.Url = _fileStorage.GetFileUrl(s.imagen);
            }
            return servicios;
        }

        public async Task<Servicio> GetByIdServicio(int id)
        {
            var s = await _repository.GetByIdServicio(id);
            if (s != null) s.Url = _fileStorage.GetFileUrl(s.imagen);
            return s;
        }

        public async Task<Servicio> AddServicio(ServicioCreateDTO dto)
        {
            string imageKey = await _fileStorage.UploadFileAsync(dto.imagenFile);
            var servicio = new Servicio
            {
                nombre = dto.nombre,
                descripcion = dto.descripcion,
                imagen = imageKey
            };
            return await _repository.AddServicio(servicio);
        }

        public async Task<UserResultDTO> UpdateServicio(int id, ServicioUpdateDTO dto)
        {
            var result = new UserResultDTO { Result = false };
            var servicio = await _repository.GetByIdServicio(id);

            if (servicio == null)
            {
                result.Message = "Servicio no encontrado";
                return result;
            }

            if (!string.IsNullOrEmpty(dto.nombre)) servicio.nombre = dto.nombre;
            if (!string.IsNullOrEmpty(dto.descripcion)) servicio.descripcion = dto.descripcion;

            if (dto.imagenFile != null && dto.imagenFile.Length > 0)
            {
                servicio.imagen = await _fileStorage.UploadFileAsync(dto.imagenFile);
            }

            await _repository.UpdateServicio(servicio);
            result.Result = true;
            result.Message = "Servicio actualizado";
            return result;
        }

        public async Task<UserResultDTO> DeleteServicio(int id)
        {
            var result = new UserResultDTO { Result = false };
            var servicio = await _repository.GetByIdServicio(id);
            if (servicio == null)
            {
                result.Message = "Servicio no encontrado";
                return result;
            }

            await _repository.DeleteServicio(id);
            result.Result = true;
            result.Message = "Servicio eliminado";
            return result;
        }
    }
}