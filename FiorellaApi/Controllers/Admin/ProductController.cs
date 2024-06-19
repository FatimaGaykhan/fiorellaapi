using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FiorellaApi.DTOs.Blogs;
using FiorellaApi.DTOs.Products;
using FiorellaApi.Helpers.Extensions;
using FiorellaApi.Models;
using FiorellaApi.Services;
using FiorellaApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace FiorellaApi.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService,
                                IMapper mapper,
                                IWebHostEnvironment env,
                                ICategoryService categoryService)
        {
            _productService = productService;
            _mapper = mapper;
            _env = env;
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            foreach (var item in request.CreateImages)
            {
                if (!item.CheckFileSize(500))
                {
                    return BadRequest();
                }

                if (!item.CheckFileType("image/"))
                {
                    return BadRequest();
                }
            }


            List<ProductImage> images = new();

            
            foreach (var item in request.CreateImages)
            {
                string fileName = $"{Guid.NewGuid()}-{item.FileName}";
                string path = _env.GenerateFilePath("img", fileName);
                await item.SaveFileToLocalAsync(path);

                images.Add(new ProductImage { Name = fileName });
            }

            images.FirstOrDefault().IsMain = true;

            var categoryId = request.CategoryId;
            var res = await _categoryService.GetByIdAsync(categoryId);
            if (res is null) return NotFound("CategoryId is Not Found");

            Product product = _mapper.Map<Product>(request);

            product.ProductImages = images;

            //string fileName = Guid.NewGuid().ToString() + "-" + request.CreateImage.FileName;

            //string path = _env.GenerateFilePath("img", fileName);

            //await request.CreateImage.SaveFileToLocalAsync(path);

            //request.Image = fileName;

            await _productService.CreateAsync(product);

            return CreatedAtAction(nameof(Create), request);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<ProductDto>>(await _productService.GetAllAsync()));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existProduct = await _productService.GetByIdWithAllDatas((int)id);

            if (existProduct is null) return NotFound();

            foreach (var item in existProduct.ProductImages)
            {
                string path = _env.GenerateFilePath("img", item.Name);
                path.DeleteFileFromLocal();
            }

            await _productService.DeleteAsync(existProduct);

            return Ok();

        }




    }
}

