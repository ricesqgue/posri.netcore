﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PosRi.Models.Request.Brand;
using PosRi.Models.Response;
using PosRi.Services.Contracts;


namespace PosRi.Controllers
{
    [Produces("application/json")]
    [Route("api/brand")]
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ILogger<BrandController> _logger;

        private const string Route = "api/brand";

        public BrandController(IBrandRepository brandRepository, ILogger<BrandController> logger)
        {
            _brandRepository = brandRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            try
            {
                var brands = await _brandRepository.GetBrandsAsync();
                var results = Mapper.Map<IEnumerable<BrandDto>>(brands);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrand([FromRoute] int id)
        {
            try
            {
                var brand = await _brandRepository.GetBrandAsync(id);
                if (brand == null)
                    return NotFound();

                var result = Mapper.Map<BrandDto>(brand);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route}/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBrand([FromBody] NewBrandDto newBrand)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _brandRepository.IsDuplicateBrandAsync(newBrand))
                {
                    ModelState.AddModelError("brand", "Brand already exists");
                    return BadRequest(ModelState);
                }

                var brandId = await _brandRepository.AddBrandAsync(newBrand);

                if (brandId > 0)
                {
                    return Ok(brandId);
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
        public async Task<IActionResult> EditBrand([FromBody] EditBrandDto brand)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _brandRepository.BrandExistsAsync(brand.Id))
                {
                    ModelState.AddModelError("brand", "Brand not found");
                    return BadRequest(ModelState);
                }

                if (await _brandRepository.IsDuplicateBrandAsync(brand))
                {
                    ModelState.AddModelError("brand", "Brand already exists");
                    return BadRequest(ModelState);
                }

                var wasBrandEdited = await _brandRepository.EditBrandAsync(brand);

                if (wasBrandEdited)
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
        public async Task<IActionResult> DeleteBrand([FromRoute]int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _brandRepository.BrandExistsAsync(id))
                {
                    return NotFound();
                }

                var wasBrandDeleted = await _brandRepository.DeleteBrandAsync(id);

                if (wasBrandDeleted)
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