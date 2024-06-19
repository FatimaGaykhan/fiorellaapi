using System;
namespace FiorellaApi.DTOs.SliderInfos
{
	public class SliderInfoEditDto
	{
        public string Title { get; set; }

        public string Description { get; set; }


        public IFormFile NewImage { get; set; }
    }
}

