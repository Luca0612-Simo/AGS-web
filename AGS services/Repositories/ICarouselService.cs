using AGS_models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGS_services.Repositories
{
    public interface ICarouselService
    {
        Task<Carrusel> AddImageToCarouselAsync(IFormFile file, string? title, int sortOrder);
        Task<IEnumerable<Carrusel>> GetImages();
    }
}
