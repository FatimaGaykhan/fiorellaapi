using System;
using AutoMapper;
using FiorellaApi.DTOs.Blogs;
using FiorellaApi.DTOs.Categories;
using FiorellaApi.DTOs.Products;
using FiorellaApi.DTOs.SliderInfos;
using FiorellaApi.DTOs.Sliders;
using FiorellaApi.Models;

namespace FiorellaApi.Helpers
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			CreateMap<BlogCreateDto, Blog>();
            CreateMap<Blog, BlogDto>();
            CreateMap<Blog, BlogDetailDto>();
            CreateMap<BlogEditDto, Blog>();

            CreateMap<SliderInfoCreateDto, SliderInfo>();
            CreateMap<SliderInfo, SliderInfoDto>();
            CreateMap<SliderInfo, SliderInfoDetailDto>();
            CreateMap<SliderInfoEditDto, SliderInfo>();

            CreateMap<SliderCreateDto, Slider>();
            CreateMap<Slider, SliderDto>();
            CreateMap<SliderEditDto, Slider>();

            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryEditDto, Category>();

            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductImage, ProductImageDto>();
            CreateMap<ProductEditDto, Product>();
            CreateMap<Product, ProductDetailDto>()
                .ForMember(dest=>dest.Category,opt=>opt.MapFrom(src=>src.Category.Name));




        }
    }
}

