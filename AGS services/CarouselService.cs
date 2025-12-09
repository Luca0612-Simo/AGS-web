using AGS_models;
using AGS_services.Repositories;
using Microsoft.AspNetCore.Http;

namespace AGS_services
{
    public class CarouselService : ICarouselService
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly iCarouselRepository _carouselRepository;

        public CarouselService(IFileStorageService fileStorageService, iCarouselRepository carouselRepository)
        {
            _fileStorageService = fileStorageService;
            _carouselRepository = carouselRepository;
        }

        public async Task<Carrusel> AddImageToCarouselAsync(IFormFile file, string? title, int sortOrder)
        {
            string imageKey = await _fileStorageService.UploadFileAsync(file);

            var newImage = new Carrusel
            {
                ImageKey = imageKey,
                Nombre = title,
                Orden = sortOrder
            };

            return await _carouselRepository.AddAsync(newImage);
        }

        public async Task<IEnumerable<Carrusel>> GetImages()
        {
            var images = await _carouselRepository.GetAllAsync();

            foreach (var image in images)
            {
                image.Url = _fileStorageService.GetFileUrl(image.ImageKey);
            }

            return images;
        }
    }
}