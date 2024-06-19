using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FiorellaApi.DTOs.Blogs;
using FiorellaApi.DTOs.Categories;
using FiorellaApi.Models;
using FiorellaApi.Services;
using FiorellaApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace FiorellaApi.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService,
                                  IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            bool existCategory = await _categoryService.ExistAsync(request.Name);

            if (existCategory)
            {
                return BadRequest();
            }

            await _categoryService.CreateAsync(_mapper.Map<Category>(request));

            return CreatedAtAction(nameof(Create), request);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<CategoryDto>>(await _categoryService.GetAllAsync()));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int? id)
        {
            if(id is null) return BadRequest();

            var response = await _categoryService.GetByIdAsync((int)id);

            if (response is null) return NotFound();

            return Ok(_mapper.Map<CategoryDto>(response));
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int? id, [FromBody]CategoryEditDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id is null) return BadRequest();

            if (await _categoryService.ExistExceptByIdAsync((int)id, request.Name))
            {
                return BadRequest();
            }

            var category = await _categoryService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            if (category.Name == request.Name)
            {
                _mapper.Map(request, category);

                await _categoryService.EditAsync(category);

                return Ok();
            }

            _mapper.Map(request, category);

            await _categoryService.EditAsync(category);

            return Ok();
        }
    }
}

