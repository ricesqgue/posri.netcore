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
        private readonly IColorRepository _colorRepository;
        private readonly ILogger<ColorController> _logger;

        private const string Route = "api/color";

        public ColorController(IColorRepository colorRepository, ILogger<ColorController> logger)
        {
            _colorRepository = colorRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetColors()
        {
            try
            {
                var colors = await _colorRepository.GetColorsAsync();
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
                var color = await _colorRepository.GetColorAsync(id);
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

                if (await _colorRepository.IsDuplicateColorAsync(newColor))
                {
                    ModelState.AddModelError("color", "Color already exists");
                    return BadRequest(ModelState);
                }

                var colorId = await _colorRepository.AddColorAsync(newColor);

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

                if (!await _colorRepository.ColorExistsAsync(color.Id))
                {
                    ModelState.AddModelError("color", "Color not found");
                    return BadRequest(ModelState);
                }

                if (await _colorRepository.IsDuplicateColorAsync(color))
                {
                    ModelState.AddModelError("color", "Color already exists");
                    return BadRequest(ModelState);
                }

                var wasColorEdited = await _colorRepository.EditColorAsync(color);

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

                if (!await _colorRepository.ColorExistsAsync(id))
                {
                    return NotFound();
                }

                var wasColorDeleted = await _colorRepository.DeleteColorAsync(id);

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