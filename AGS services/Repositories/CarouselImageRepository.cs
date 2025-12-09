using AGS_models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AGS_services.Repositories.CarouselImageRepository;

namespace AGS_services.Repositories
{

        public class CarouselImageRepository : iCarouselRepository
        {
            private readonly ApplicationDbContext _context;

            public CarouselImageRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Carrusel> AddAsync(Carrusel image)
            {
                await _context.Carrusel.AddAsync(image);
                await _context.SaveChangesAsync();
                return image;
            }

            public async Task<IEnumerable<Carrusel>> GetAllAsync()
            {
                return await _context.Carrusel.OrderBy(img => img.Orden).ToListAsync();
            }
        }
    }

