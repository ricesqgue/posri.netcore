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
        private readonly ISizeRepository _sizeService;
        private readonly ILogger<SizeController> _logger;

        private const string Route = "api/size";

        public SizeController(ISizeRepository sizeService, ILogger<SizeController> logger)
        {
            _sizeService = sizeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetSizes()
        {
            try
            {
                var sizes = await _sizeService.GetSizesAsync();
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
                var size = await _sizeService.GetSizeAsync(id);
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

                if (await _sizeService.IsDuplicateSizeAsync(newSize))
                {
                    ModelState.AddModelError("size", "Size already exists");
                    return BadRequest(ModelState);
                }

                var sizeId = await _sizeService.AddSizeAsync(newSize);

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

                if (!await _sizeService.SizeExistsAsync(size.Id))
                {
                    ModelState.AddModelError("size", "Size not found");
                    return BadRequest(ModelState);
                }

                if (await _sizeService.IsDuplicateSizeAsync(size))
                {
                    ModelState.AddModelError("size", "Size already exists");
                    return BadRequest(ModelState);
                }

                var wasSizeEdited = await _sizeService.EditSizeAsync(size);

                if (wasSizeEdited)
                {
                    return Ok();
                }

                return NoContent();

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

                if (!await _sizeService.SizeExistsAsync(id))
                {
                    return NotFound();
                }

                var wasSizeDeleted = await _sizeService.DeleteSizeAsync(id);

                if (wasSizeDeleted)
                {
                    return Ok();
                }

                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogCritical($"DELETE {Route}/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }
    }
}