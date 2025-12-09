using AGS_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGS_services.Repositories
{
    public interface iCarouselRepository
    {
        Task<Carrusel> AddAsync(Carrusel imagen);
        Task<IEnumerable<Carrusel>> GetAllAsync();
    }
}
