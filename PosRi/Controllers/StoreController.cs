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
        private readonly IStoreRepository _storeRepository;
        private readonly ICashRegisterRepository _cashRegisterRepository;
        private readonly ILogger<StoreController> _logger;

        private const string Route = "api/store";

        public StoreController(IStoreRepository storeRepository, ICashRegisterRepository cashRegisterRepository, ILogger<StoreController> logger)
        {
            _storeRepository = storeRepository;
            _cashRegisterRepository = cashRegisterRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetStores()
        {
            try
            {
                var stores = await _storeRepository.GetStoresAsync();
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
                var store = await _storeRepository.GetStoreAsync(id);
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
                var users = await _storeRepository.GetUsersByStoreAsync(id);
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

                if (await _storeRepository.IsDuplicateStoreAsync(newStore))
                {
                    ModelState.AddModelError("store", "Store already exists");
                    return BadRequest(ModelState);
                }

                var storeId = await _storeRepository.AddStoreAsync(newStore);

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

                if (!await _storeRepository.StoreExistsAsync(store.Id))
                {
                    ModelState.AddModelError("store", "Store not found");
                    return BadRequest(ModelState);
                }

                if (await _storeRepository.IsDuplicateStoreAsync(store))
                {
                    ModelState.AddModelError("store", "Store already exists");
                    return BadRequest(ModelState);
                }

                var wasStoreEdited = await _storeRepository.EditStoreAsync(store);

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

                if (!await _storeRepository.StoreExistsAsync(id))
                {
                    return NotFound();
                }

                var wasStoreDeleted = await _storeRepository.DeleteStoreAsync(id);

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
                if (!await _storeRepository.StoreExistsAsync(storeId))
                {
                    return NotFound();
                }
                var cashRegisters = await _cashRegisterRepository.GetCashRegistersAsync(storeId);
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
                if (!await _storeRepository.StoreExistsAsync(storeId))
                {
                    return NotFound();
                }
                var cashRegister = await _cashRegisterRepository.GetCashRegisterAsync(id);
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

                if (!await _storeRepository.StoreExistsAsync(storeId))
                {
                    return NotFound();
                }

                if (await _cashRegisterRepository.IsDuplicateCashRegisterAsync(storeId, newCashRegister))
                {
                    ModelState.AddModelError("cashRegister", "Cash register already exists");
                    return BadRequest(ModelState);
                }

                var cashRegisterId = await _cashRegisterRepository.AddCashRegisterAsync(storeId, newCashRegister);

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

                if (!await _storeRepository.StoreExistsAsync(storeId))
                {
                    return NotFound();
                }

                if (!await _cashRegisterRepository.CashRegisterExistsAsync(cashRegister.Id))
                {
                    ModelState.AddModelError("cashRegister", "Cash register not found");
                    return BadRequest(ModelState);
                }

                if (await _cashRegisterRepository.IsDuplicateCashRegisterAsync(storeId, cashRegister))
                {
                    ModelState.AddModelError("cashRegister", "Cash register already exists");
                    return BadRequest(ModelState);
                }

                var wasCashRegisterEdited = await _cashRegisterRepository.EditCashRegisterAsync(cashRegister);

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

                if (!await _storeRepository.StoreExistsAsync(storeId))
                {
                    return NotFound();
                }

                if (!await _cashRegisterRepository.CashRegisterExistsAsync(id))
                {
                    return NotFound();
                }

                var wasCashRegisterDeleted = await _cashRegisterRepository.DeleteCashRegisterAsync(id);

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