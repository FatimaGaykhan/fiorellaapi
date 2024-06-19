using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using AutoMapper;
using FiorellaApi.DTOs.Blogs;
using FiorellaApi.Helpers.Extensions;
using FiorellaApi.Models;
using FiorellaApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FiorellaApi.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    public class BlogController : ControllerBase
    {
        public readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public BlogController(IBlogService blogService,
                             IWebHostEnvironment env,
                             IMapper mapper)
        {
            _blogService = blogService;
            _env = env;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BlogCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!request.CreateImage.CheckFileType("image/"))
            {
                return BadRequest(ModelState);
            }

            if (!request.CreateImage.CheckFileSize(200))
            {
                return BadRequest(ModelState);
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.CreateImage.FileName;

            string path = _env.GenerateFilePath("img", fileName);

            await request.CreateImage.SaveFileToLocalAsync(path);

            request.Image = fileName;

            await _blogService.CreateAsync(_mapper.Map<Blog>(request));

            return CreatedAtAction(nameof(Create), request);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<BlogDto>>(await _blogService.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int? id)
        {
            if (id is null) return BadRequest();

            var response = await _blogService.GetByIdAsync((int)id);

            if (response is null) return NotFound();

            return Ok(_mapper.Map<BlogDto>(response));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail([FromRoute]int? id)
        {
            if (id is null) return BadRequest();

            var response = await _blogService.DetailAsync((int)id);

            if (response is null) return NotFound();

            return Ok(_mapper.Map<BlogDetailDto>(response));

        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int? id)
        {
            if (id is null) return BadRequest();

            var response = await _blogService.GetByIdAsync((int)id);

            if (response is null) return NotFound();

            string path = _env.GenerateFilePath("img", response.Image);

            path.DeleteFileFromLocal();

            await _blogService.DeleteAsync(response);

            return Ok();

        }

        [HttpPut]
        public async Task<IActionResult> Update(int ? id, [FromForm] BlogEditDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id is null) return BadRequest();

            var entity = await _blogService.GetByIdAsync((int)id);

            if (entity is null) return NotFound();

            if (request.NewImage != null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {

                    return BadRequest();
                }

                if (!request.NewImage.CheckFileSize(200))
                {

                    return BadRequest();
                }

                string oldPath = _env.GenerateFilePath("img", entity.Image);

                oldPath.DeleteFileFromLocal();

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;


                string newPath = _env.GenerateFilePath("img", fileName);

                await request.NewImage.SaveFileToLocalAsync(newPath);

                entity.Image = fileName;
            }



            _mapper.Map(request, entity);

            await _blogService.EditAsync(entity);

            return Ok();

        }



    }
}

