using System;
using FiorellaApi.Models;

namespace FiorellaApi.Services.Interfaces
{
	public interface ISliderInfoService
	{
        Task CreateAsync(SliderInfo sliderInfo);
        Task<IEnumerable<SliderInfo>> GetAllAsync();
        Task<SliderInfo> GetByIdAsync(int id);
        Task<SliderInfo> DetailAsync(int id);
        Task DeleteAsync(SliderInfo sliderInfo);
        Task EditAsync(SliderInfo sliderInfo);
    }
}

