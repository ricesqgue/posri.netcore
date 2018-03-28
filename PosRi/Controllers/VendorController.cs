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
        private readonly IVendorService _vendorService;
        private readonly ILogger<VendorController> _logger;

        private const string Route = "api/vendor";

        public VendorController(IVendorService vendorService, ILogger<VendorController> logger)
        {
            _vendorService = vendorService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetVendors()
        {
            try
            {
                var vendors = await _vendorService.GetVendors();
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
                var vendor = await _vendorService.GetVendor(id);
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

                if (await _vendorService.IsDuplicateVendor(newVendor))
                {
                    ModelState.AddModelError("vendor", "Vendor already exists");
                    return BadRequest(ModelState);
                }

                var vendorId = await _vendorService.AddVendor(newVendor);

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

                if (!await _vendorService.VendorExists(vendor.Id))
                {
                    ModelState.AddModelError("vendor", "Vendor not found");
                    return BadRequest(ModelState);
                }

                if (await _vendorService.IsDuplicateVendor(vendor))
                {
                    ModelState.AddModelError("vendor", "Vendor already exists");
                    return BadRequest(ModelState);
                }

                var wasVendorEdited = await _vendorService.EditVendor(vendor);

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

                if (!await _vendorService.VendorExists(id))
                {
                    return NotFound();
                }

                var wasVendorDeleted = await _vendorService.DeleteVendor(id);

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