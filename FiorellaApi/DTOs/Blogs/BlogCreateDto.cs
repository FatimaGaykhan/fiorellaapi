using System;
namespace FiorellaApi.DTOs.Blogs
{
	public class BlogCreateDto
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public IFormFile CreateImage { get; set; }

		public string Image { get; set; }

	}
}

