using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FiorellaApi.DTOs.SliderInfos;
using FiorellaApi.DTOs.Sliders;
using FiorellaApi.Helpers.Extensions;
using FiorellaApi.Models;
using FiorellaApi.Services;
using FiorellaApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace FiorellaApi.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    public class SliderController : ControllerBase
    {
        private readonly ISliderService _sliderService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public SliderController(ISliderService sliderService,
                               IMapper mapper,
                               IWebHostEnvironment env)
        {
            _sliderService = sliderService;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<SliderDto>>(await _sliderService.GetAllAsync()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SliderCreateDto request)
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

            await _sliderService.CreateAsync(_mapper.Map<Slider>(request));

            return CreatedAtAction(nameof(Create), request);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id is null) return BadRequest();

            var response = await _sliderService.GetByIdAsync((int)id);

            if (response is null) return NotFound();

            string path = _env.GenerateFilePath("img", response.Image);

            path.DeleteFileFromLocal();

            await _sliderService.DeleteAsync(response);

            return Ok();

        }

        [HttpPut]
        public async Task<IActionResult> Update(int? id, [FromForm] SliderEditDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id is null) return BadRequest();

            var entity = await _sliderService.GetByIdAsync((int)id);

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

            await _sliderService.EditAsync(entity);

            return Ok();

        }
    }

}

