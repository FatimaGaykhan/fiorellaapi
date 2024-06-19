using System;
using FiorellaApi.Models;

namespace FiorellaApi.DTOs.Products
{
	public class ProductCreateDto
	{
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public List<IFormFile> CreateImages { get; set; }


    }
}

