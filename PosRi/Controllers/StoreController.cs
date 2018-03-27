using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PosRi.Models.Request;
using PosRi.Models.Request.CashRegister;
using PosRi.Models.Request.Store;
using PosRi.Models.Response;
using PosRi.Services.Contracts;

namespace PosRi.Controllers
{
    [Produces("application/json")]
    [Route("api/store")]
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly ICashRegisterService _cashRegisterService;
        private readonly ILogger<StoreController> _logger;

        private const string Route = "api/store";

        public StoreController(IStoreService storeService, ICashRegisterService cashRegisterService, ILogger<StoreController> logger)
        {
            _storeService = storeService;
            _cashRegisterService = cashRegisterService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetStores()
        {
            try
            {
                var stores = await _storeService.GetStores();
                var results = Mapper.Map<IEnumerable<StoreDto>>(stores);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStore([FromRoute] int id)
        {
            try
            {
                var store = await _storeService.GetStore(id);
                if (store == null)
                    return NotFound();

                var result = Mapper.Map<StoreDto>(store);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route}/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpGet("{id}/users")]
        public async Task<IActionResult> GetUsersByStore([FromRoute]int id)
        {
            try
            {
                var users = await _storeService.GetUsersByStore(id);
                var results = Mapper.Map<IEnumerable<UserDto>>(users);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route}/{id}/users - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddStore([FromBody] NewStoreDto newStore)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _storeService.IsDuplicateStore(newStore))
                {
                    ModelState.AddModelError("store", "Store already exists");
                    return BadRequest(ModelState);
                }

                var storeId = await _storeService.AddStore(newStore);

                if (storeId > 0)
                {
                    return Ok(storeId);
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
        public async Task<IActionResult> EditStore([FromBody] EditStoreDto store)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _storeService.StoreExists(store.Id))
                {
                    ModelState.AddModelError("store", "Store not found");
                    return BadRequest(ModelState);
                }

                if (await _storeService.IsDuplicateStore(store))
                {
                    ModelState.AddModelError("store", "Store already exists");
                    return BadRequest(ModelState);
                }

                var wasStoreEdited = await _storeService.EditStore(store);

                if (wasStoreEdited)
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
        public async Task<IActionResult> DeleteStore([FromRoute]int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _storeService.StoreExists(id))
                {
                    return NotFound();
                }

                var wasStoreDeleted = await _storeService.DeleteStore(id);

                if (wasStoreDeleted)
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

        [HttpGet("{storeId}/cashregisters")]
        public async Task<IActionResult> GetCashRegisters([FromRoute] int storeId)
        {
            try
            {
                if (!await _storeService.StoreExists(storeId))
                {
                    return NotFound();
                }
                var cashRegisters = await _cashRegisterService.GetCashRegisters(storeId);
                var results = Mapper.Map<IEnumerable<CashRegisterDto>>(cashRegisters);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route}/{storeId}/cashregisters - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpGet("{storeId}/cashregisters/{id}")]
        public async Task<IActionResult> GetCashRegister([FromRoute] int storeId, [FromRoute] int id)
        {
            try
            {
                if (!await _storeService.StoreExists(storeId))
                {
                    return NotFound();
                }
                var cashRegister = await _cashRegisterService.GetCashRegister(id);
                if (cashRegister == null)
                    return NotFound();

                var result = Mapper.Map<CashRegisterDto>(cashRegister);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route}/{storeId}/cashregisters/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpPost("{storeId}/cashregisters")]
        public async Task<IActionResult> AddCashRegister([FromRoute] int storeId, [FromBody] NewCashRegisterDto newCashRegister)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _storeService.StoreExists(storeId))
                {
                    return NotFound();
                }

                if (await _cashRegisterService.IsDuplicateCashRegister(storeId, newCashRegister))
                {
                    ModelState.AddModelError("cashRegister", "Cash register already exists");
                    return BadRequest(ModelState);
                }

                var cashRegisterId = await _cashRegisterService.AddCashRegister(storeId, newCashRegister);

                if (cashRegisterId > 0)
                {
                    return Ok(cashRegisterId);
                }

                return StatusCode(500, "An error ocurred in server");
            }
            catch (Exception e)
            {
                _logger.LogCritical($"POST {Route}/{storeId}/cashregisters - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpPut("{storeId}/cashregisters")]
        public async Task<IActionResult> EditCashRegister([FromRoute] int storeId, [FromBody] EditCashRegisterDto cashRegister)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _storeService.StoreExists(storeId))
                {
                    return NotFound();
                }

                if (!await _cashRegisterService.CashRegisterExists(cashRegister.Id))
                {
                    ModelState.AddModelError("cashRegister", "Cash register not found");
                    return BadRequest(ModelState);
                }

                if (await _cashRegisterService.IsDuplicateCashRegister(storeId, cashRegister))
                {
                    ModelState.AddModelError("cashRegister", "Cash register already exists");
                    return BadRequest(ModelState);
                }

                var wasCashRegisterEdited = await _cashRegisterService.EditCashRegister(cashRegister);

                if (wasCashRegisterEdited)
                {
                    return Ok();
                }

                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogCritical($"PUT {Route}/{storeId}/cashregisters - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpDelete("{storeId}/cashregisters/{id}")]
        public async Task<IActionResult> DeleteCashRegister([FromRoute] int storeId, [FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _storeService.StoreExists(storeId))
                {
                    return NotFound();
                }

                if (!await _cashRegisterService.CashRegisterExists(id))
                {
                    return NotFound();
                }

                var wasCashRegisterDeleted = await _cashRegisterService.DeleteCashRegister(id);

                if (wasCashRegisterDeleted)
                {
                    return Ok();
                }

                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogCritical($"DELETE {Route}/{storeId}/cashregisters/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

    }
}