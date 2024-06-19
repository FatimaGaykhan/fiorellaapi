using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FiorellaApi.DTOs.Blogs;
using FiorellaApi.DTOs.SliderInfos;
using FiorellaApi.Helpers.Extensions;
using FiorellaApi.Models;
using FiorellaApi.Services;
using FiorellaApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FiorellaApi.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    public class SliderInfoController : ControllerBase
    {
        public readonly ISliderInfoService _sliderInfoService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public SliderInfoController(ISliderInfoService sliderInfoService,
                             IWebHostEnvironment env,
                             IMapper mapper)
        {
            _sliderInfoService = sliderInfoService;
            _env = env;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SliderInfoCreateDto request)
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

            await _sliderInfoService.CreateAsync(_mapper.Map<SliderInfo>(request));

            return CreatedAtAction(nameof(Create), request);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<SliderInfoDto>>(await _sliderInfoService.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int? id)
        {
            if (id is null) return BadRequest();

            var response = await _sliderInfoService.GetByIdAsync((int)id);

            if (response is null) return NotFound();

            return Ok(_mapper.Map<SliderInfoDto>(response));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail([FromRoute] int? id)
        {
            if (id is null) return BadRequest();

            var response = await _sliderInfoService.DetailAsync((int)id);

            if (response is null) return NotFound();

            return Ok(_mapper.Map<SliderInfoDetailDto>(response));

        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id is null) return BadRequest();

            var response = await _sliderInfoService.GetByIdAsync((int)id);

            if (response is null) return NotFound();

            string path = _env.GenerateFilePath("img", response.Image);

            path.DeleteFileFromLocal();

            await _sliderInfoService.DeleteAsync(response);

            return Ok();

        }

        [HttpPut]
        public async Task<IActionResult> Update(int? id, [FromForm] SliderInfoEditDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id is null) return BadRequest();

            var entity = await _sliderInfoService.GetByIdAsync((int)id);

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

            await _sliderInfoService.EditAsync(entity);

            return Ok();

        }
    }
}

