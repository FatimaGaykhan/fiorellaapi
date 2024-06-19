using System;
using FiorellaApi.Models;

namespace FiorellaApi.DTOs.Products
{
	public class ProductDetailDto
	{
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public ICollection<ProductImageDto> ProductImages { get; set; }
    }
}

