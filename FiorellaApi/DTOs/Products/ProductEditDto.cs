using System;
using FiorellaApi.Models;

namespace FiorellaApi.DTOs.Products
{
	public class ProductEditDto
	{
        public string Name { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public int CategoryId { get; set; }

        public List<ProductImageDto> Images { get; set; }

        public List<IFormFile> NewImages { get; set; }
    }
}

