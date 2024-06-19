using System;
using FiorellaApi.Models;

namespace FiorellaApi.Services.Interfaces
{
	public interface ISliderService
	{
        Task<IEnumerable<Slider>> GetAllAsync();
        Task<SliderInfo> GetSliderInfoAsync();
        Task CreateAsync(Slider slider);
        Task DeleteAsync(Slider slider);
        Task<Slider> GetByIdAsync(int id);
        Task EditAsync(Slider slider);

    }
}

