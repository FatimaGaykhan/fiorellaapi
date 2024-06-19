using System;
namespace FiorellaApi.DTOs.Sliders
{
	public class SliderCreateDto
	{
        public IFormFile CreateImage { get; set; }

        public string Image { get; set; }
    }
}

