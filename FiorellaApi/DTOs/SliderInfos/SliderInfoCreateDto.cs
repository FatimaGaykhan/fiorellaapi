using System;
namespace FiorellaApi.DTOs.SliderInfos
{
	public class SliderInfoCreateDto
	{
        public string Title { get; set; }

        public string Description { get; set; }

        public IFormFile CreateImage { get; set; }

        public string Image { get; set; }
    }
}

