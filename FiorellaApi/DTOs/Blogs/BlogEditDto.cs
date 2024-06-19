using System;
namespace FiorellaApi.DTOs.Blogs
{
	public class BlogEditDto
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public IFormFile NewImage { get; set; }


	}
}

