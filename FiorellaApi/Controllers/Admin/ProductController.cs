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

        [HttpPut]
        public async Task<IActionResult> Edit(int? id , [FromForm] ProductEditDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id is null) return BadRequest();

            var entity = await _productService.GetByIdAsync((int)id);

            if (entity is null) return NotFound();

            List<ProductImageDto> images = new();

            

            if (!ModelState.IsValid)
            {

                return Ok(new ProductEditDto { Images = images });

            }

            List<ProductImage> newImages = new();

            if (request.NewImages is not null)
            {
                foreach (var item in request.NewImages)
                {


                    if (!item.CheckFileType("image/"))
                    {
                        return BadRequest();

                    }
                    if (!item.CheckFileSize(500))
                    {
                        return BadRequest();
                    }


                }

                if (request.NewImages is not null)
                {

                    foreach (var item in request.NewImages)
                    {
                        string oldPath = _env.GenerateFilePath("img", item.Name);
                        oldPath.DeleteFileFromLocal();
                        string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;
                        string newPath = _env.GenerateFilePath("img", fileName);

                        await item.SaveFileToLocalAsync(newPath);


                        entity.ProductImages.Add(new ProductImage { Name = fileName });

                    }
                }
            }

            if (request.Name is not null)
            {
                entity.Name = request.Name;
            }
            if (request.Description is not null)
            {
                entity.Description = request.Description;
            }

            if (request.CategoryId != entity.CategoryId)
            {
                entity.CategoryId = request.CategoryId;
            }


            if (decimal.Parse(request.Price.Replace(".", ",")) != entity.Price)
            {
                entity.Price = decimal.Parse(request.Price.Replace(".", ","));
            }




            _mapper.Map(request, entity);

            await _productService.EditAsync(entity);

            return Ok();

        }

        //[HttpPut]
        //public async Task<IActionResult> Update(int? id, [FromForm] BlogEditDto request)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    if (id is null) return BadRequest();

        //    var entity = await _blogService.GetByIdAsync((int)id);

        //    if (entity is null) return NotFound();

        //    if (request.NewImage != null)
        //    {
        //        if (!request.NewImage.CheckFileType("image/"))
        //        {

        //            return BadRequest();
        //        }

        //        if (!request.NewImage.CheckFileSize(200))
        //        {

        //            return BadRequest();
        //        }

        //        string oldPath = _env.GenerateFilePath("img", entity.Image);

        //        oldPath.DeleteFileFromLocal();

        //        string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;


        //        string newPath = _env.GenerateFilePath("img", fileName);

        //        await request.NewImage.SaveFileToLocalAsync(newPath);

        //        entity.Image = fileName;
        //    }



        //    _mapper.Map(request, entity);

        //    await _blogService.EditAsync(entity);

        //    return Ok();

        //}




    }
}

