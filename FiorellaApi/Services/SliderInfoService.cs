using System;
using System.Reflection.Metadata;
using FiorellaApi.Data;
using FiorellaApi.Models;
using FiorellaApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorellaApi.Services
{
	public class SliderInfoService:ISliderInfoService
	{
        private readonly AppDbContext _context;

		public SliderInfoService(AppDbContext context)
		{
            _context = context;
		}

        public async Task CreateAsync(SliderInfo sliderInfo)
        {
            await _context.SliderInfos.AddAsync(sliderInfo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(SliderInfo sliderInfo)
        {
            _context.SliderInfos.Remove(sliderInfo);

            await _context.SaveChangesAsync();
        }

        public async Task<SliderInfo> DetailAsync(int id)
        {
            return await _context.SliderInfos.AsNoTracking().Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task EditAsync(SliderInfo sliderInfo)
        {
            _context.SliderInfos.Update(sliderInfo);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SliderInfo>> GetAllAsync()
        {
            return await _context.SliderInfos.AsNoTracking().ToListAsync();
        }

        public async Task<SliderInfo> GetByIdAsync(int id)
        {
            return await _context.SliderInfos.AsNoTracking().Where(m => m.Id == id).FirstOrDefaultAsync();

        }
    }
}

