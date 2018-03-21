using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosRi.Models.Request;
using PosRi.Models.Response;
using PosRi.Services.Contracts;

namespace PosRi.Controllers
{
    [Produces("application/json")]
    [Route("api/store")]
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStores()
        {
            var stores = await _storeService.GetStores();
            var results = Mapper.Map<IEnumerable<StoreDto>>(stores);
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStore(int id)
        {
            var store = await _storeService.GetStore(id);
            if (store == null)
                return NotFound();

            var result = Mapper.Map<StoreDto>(store);

            return Ok(result);
        }

        [HttpGet("{id}/users")]
        public async Task<IActionResult> GetUsersByStore(int id)
        {
            var users = await _storeService.GetUsersByStore(id);
            var results = Mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> AddStore(NewStoreDto newStore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _storeService.StoreExists(newStore))
            {
                return BadRequest();
            }

            var storeId = await _storeService.AddStore(newStore);

            if (storeId > 0)
            {
                return Ok(storeId);
            }

            return StatusCode(500, "An error ocurred in server");
        }
    }
}