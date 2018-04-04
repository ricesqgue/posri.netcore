using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PosRi.Models.Request.Color;
using PosRi.Models.Response;
using PosRi.Services.Contracts;

namespace PosRi.Controllers
{
    [Produces("application/json")]
    [Route("api/color")]
    public class ColorController : Controller
    {
        private readonly IColorRepository _colorService;
        private readonly ILogger<ColorController> _logger;

        private const string Route = "api/color";

        public ColorController(IColorRepository colorService, ILogger<ColorController> logger)
        {
            _colorService = colorService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetColors()
        {
            try
            {
                var colors = await _colorService.GetColorsAsync();
                var results = Mapper.Map<IEnumerable<ColorDto>>(colors);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetColor([FromRoute] int id)
        {
            try
            {
                var color = await _colorService.GetColorAsync(id);
                if (color == null)
                    return NotFound();

                var result = Mapper.Map<ColorDto>(color);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route}/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddColor([FromBody] NewColorDto newColor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _colorService.IsDuplicateColorAsync(newColor))
                {
                    ModelState.AddModelError("color", "Color already exists");
                    return BadRequest(ModelState);
                }

                var colorId = await _colorService.AddColorAsync(newColor);

                if (colorId > 0)
                {
                    return Ok(colorId);
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
        public async Task<IActionResult> EditColor([FromBody] EditColorDto color)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _colorService.ColorExistsAsync(color.Id))
                {
                    ModelState.AddModelError("color", "Color not found");
                    return BadRequest(ModelState);
                }

                if (await _colorService.IsDuplicateColorAsync(color))
                {
                    ModelState.AddModelError("color", "Color already exists");
                    return BadRequest(ModelState);
                }

                var wasColorEdited = await _colorService.EditColorAsync(color);

                if (wasColorEdited)
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
        public async Task<IActionResult> DeleteColor([FromRoute]int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _colorService.ColorExistsAsync(id))
                {
                    return NotFound();
                }

                var wasColorDeleted = await _colorService.DeleteColorAsync(id);

                if (wasColorDeleted)
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