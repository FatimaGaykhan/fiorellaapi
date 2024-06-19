using System;
using FiorellaApi.Data;
using FiorellaApi.Models;
using FiorellaApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorellaApi.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;

        public SliderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Slider slider)
        {
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Slider slider)
        {
            _context.Sliders.Remove(slider);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Slider slider)
        {
            _context.Sliders.Update(slider);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Slider>> GetAllAsync()
        {
            return await _context.Sliders.AsNoTracking().ToListAsync();
        }

        public async Task<Slider> GetByIdAsync(int id)
        {
            return await _context.Sliders.AsNoTracking().Where(m => m.Id == id).FirstOrDefaultAsync();

        }

        public async Task<SliderInfo> GetSliderInfoAsync()
        {
            return await _context.SliderInfos.FirstOrDefaultAsync();

        }
    }
}

