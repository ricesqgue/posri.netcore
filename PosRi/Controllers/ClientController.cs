using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PosRi.Models.Request.Client;
using PosRi.Models.Response;
using PosRi.Services.Contracts;

namespace PosRi.Controllers
{
    [Produces("application/json")]
    [Route("api/client")]
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientController> _logger;

        private const string Route = "api/client";

        public ClientController(IClientRepository clientRepository, ILogger<ClientController> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                var clients = await _clientRepository.GetClientsAsync();
                var results = Mapper.Map<IEnumerable<ClientDto>>(clients);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient([FromRoute] int id)
        {
            try
            {
                var client = await _clientRepository.GetClientAsync(id);
                if (client == null)
                    return NotFound();

                var result = Mapper.Map<ClientDto>(client);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route}/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] NewClientDto newClient)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _clientRepository.IsDuplicateClientAsync(newClient))
                {
                    ModelState.AddModelError("client", "Client already exists");
                    return BadRequest(ModelState);
                }

                var clientId = await _clientRepository.AddClientAsync(newClient);

                if (clientId > 0)
                {
                    return Ok(clientId);
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
        public async Task<IActionResult> EditClient([FromBody] EditClientDto client)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _clientRepository.ClientExistsAsync(client.Id))
                {
                    ModelState.AddModelError("client", "Client not found");
                    return BadRequest(ModelState);
                }

                if (await _clientRepository.IsDuplicateClientAsync(client))
                {
                    ModelState.AddModelError("client", "Client already exists");
                    return BadRequest(ModelState);
                }

                var wasClientEdited = await _clientRepository.EditClientAsync(client);

                if (wasClientEdited)
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
        public async Task<IActionResult> DeleteClient([FromRoute]int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _clientRepository.ClientExistsAsync(id))
                {
                    return NotFound();
                }

                var wasClientDeleted = await _clientRepository.DeleteClientAsync(id);

                if (wasClientDeleted)
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