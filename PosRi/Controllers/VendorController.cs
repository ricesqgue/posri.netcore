using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PosRi.Entities;
using PosRi.Models.Request.Client;
using PosRi.Models.Request.Vendor;
using PosRi.Models.Response;
using PosRi.Services.Contracts;

namespace PosRi.Controllers
{
    [Produces("application/json")]
    [Route("api/vendor")]
    public class VendorController : Controller
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly ILogger<VendorController> _logger;

        private const string Route = "api/vendor";

        public VendorController(IVendorRepository vendorRepository, ILogger<VendorController> logger)
        {
            _vendorRepository = vendorRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetVendors()
        {
            try
            {
                var vendors = await _vendorRepository.GetVendorsAsync();
                var results = Mapper.Map<IEnumerable<VendorDto>>(vendors);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendor([FromRoute] int id)
        {
            try
            {
                var vendor = await _vendorRepository.GetVendorAsync(id);
                if (vendor == null)
                    return NotFound();

                var result = Mapper.Map<VendorDto>(vendor);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route}/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddVendor([FromBody] NewVendorDto newVendor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _vendorRepository.IsDuplicateVendorAsync(newVendor))
                {
                    ModelState.AddModelError("vendor", "Vendor already exists");
                    return BadRequest(ModelState);
                }

                var vendorId = await _vendorRepository.AddVendorAsync(newVendor);

                if (vendorId > 0)
                {
                    return Ok(vendorId);
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
        public async Task<IActionResult> EditVendor([FromBody] EditVendorDto vendor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _vendorRepository.VendorExistsAsync(vendor.Id))
                {
                    ModelState.AddModelError("vendor", "Vendor not found");
                    return BadRequest(ModelState);
                }

                if (await _vendorRepository.IsDuplicateVendorAsync(vendor))
                {
                    ModelState.AddModelError("vendor", "Vendor already exists");
                    return BadRequest(ModelState);
                }

                var wasVendorEdited = await _vendorRepository.EditVendorAsync(vendor);

                if (wasVendorEdited)
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
        public async Task<IActionResult> DeleteVendor([FromRoute]int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _vendorRepository.VendorExistsAsync(id))
                {
                    return NotFound();
                }

                var wasVendorDeleted = await _vendorRepository.DeleteVendorAsync(id);

                if (wasVendorDeleted)
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