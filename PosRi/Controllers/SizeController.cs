using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PosRi.Models.Request.Size;
using PosRi.Models.Response;
using PosRi.Services.Contracts;

namespace PosRi.Controllers
{
    [Produces("application/json")]
    [Route("api/size")]
    public class SizeController : Controller
    {
        private readonly ISizeService _sizeService;
        private readonly ILogger<SizeController> _logger;

        private const string Route = "api/size";

        public SizeController(ISizeService sizeService, ILogger<SizeController> logger)
        {
            _sizeService = sizeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetSizes()
        {
            try
            {
                var sizes = await _sizeService.GetSizes();
                var results = Mapper.Map<IEnumerable<SizeDto>>(sizes);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSize([FromRoute] int id)
        {
            try
            {
                var size = await _sizeService.GetSize(id);
                if (size == null)
                    return NotFound();

                var result = Mapper.Map<SizeDto>(size);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route}/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSize([FromBody] NewSizeDto newSize)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _sizeService.IsDuplicateSize(newSize))
                {
                    ModelState.AddModelError("size", "Size already exists");
                    return BadRequest(ModelState);
                }

                var sizeId = await _sizeService.AddSize(newSize);

                if (sizeId > 0)
                {
                    return Ok(sizeId);
                }

                return StatusCode(500, "An error ocurred in server");
            }
            catch (Exception e)
            {
                _logger.LogCritical($"POST {Route} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditSize([FromBody] EditSizeDto size)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _sizeService.SizeExists(size.Id))
                {
                    ModelState.AddModelError("size", "Size not found");
                    return BadRequest(ModelState);
                }

                if (await _sizeService.IsDuplicateSize(size))
                {
                    ModelState.AddModelError("size", "Size already exists");
                    return BadRequest(ModelState);
                }

                var wasSizeEdited = await _sizeService.EditSize(size);

                if (wasSizeEdited)
                {
                    return NoContent();
                }

                return StatusCode(500, "An error ocurred in server");
            }
            catch (Exception e)
            {
                _logger.LogCritical($"PUT {Route} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSize([FromRoute]int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _sizeService.SizeExists(id))
                {
                    return NotFound();
                }

                var wasSizeDeleted = await _sizeService.DeleteSize(id);

                if (wasSizeDeleted)
                {
                    return NoContent();
                }

                return StatusCode(500, "An error ocurred in server");
            }
            catch (Exception e)
            {
                _logger.LogCritical($"DELETE {Route}/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }
    }
}