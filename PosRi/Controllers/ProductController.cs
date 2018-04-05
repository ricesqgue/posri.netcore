using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PosRi.Models.Request.Product;
using PosRi.Models.Response;
using PosRi.Services.Contracts;

namespace PosRi.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        private const string Route = "api/product";

        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductHeaders()
        {
            try
            {
                var productHeaders = await _productRepository.GetProductHeadersAsync();
                var results = Mapper.Map<IEnumerable<ProductHeaderDto>>(productHeaders);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductHeader([FromRoute] int id)
        {
            try
            {
                var productHeader = await _productRepository.GetProductHeaderAsync(id);
                if (productHeader == null)
                    return NotFound();

                var result = Mapper.Map<ProductHeaderDto>(productHeader);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route}/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProductHeader([FromBody] NewProductHeaderDto newProductHeader)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _productRepository.IsDuplicateProductHeaderAsync(newProductHeader))
                {
                    ModelState.AddModelError("productHeader", "A product already exists");
                    return BadRequest(ModelState);
                }

                var productHeaderId = await _productRepository.AddProductHeaderAsync(newProductHeader);

                if (productHeaderId > 0)
                {
                    return Ok(productHeaderId);
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
        public async Task<IActionResult> EditProductHeader([FromBody] EditProductHeaderDto productHeader)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _productRepository.ProductHeaderExistsAsync(productHeader.Id))
                {
                    ModelState.AddModelError("productHeader", "ProductHeader not found");
                    return BadRequest(ModelState);
                }

                if (await _productRepository.IsDuplicateProductHeaderAsync(productHeader))
                {
                    ModelState.AddModelError("productHeader", "A product already exists");
                    return BadRequest(ModelState);
                }

                var wasProductHeaderEdited = await _productRepository.EditProductHeaderAsync(productHeader);

                if (wasProductHeaderEdited)
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
        public async Task<IActionResult> DeleteProductHeader([FromRoute]int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _productRepository.ProductHeaderExistsAsync(id))
                {
                    return NotFound();
                }

                var wasProductHeaderDeleted = await _productRepository.DeleteProductHeaderAsync(id);

                if (wasProductHeaderDeleted)
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

        [HttpGet("{productHeaderId}/products")]
        public async Task<IActionResult> GetProducts([FromRoute] int productHeaderId)
        {
            try
            {
                if (!await _productRepository.ProductHeaderExistsAsync(productHeaderId))
                {
                    return NotFound();
                }
                var products = await _productRepository.GetProductsAsync(productHeaderId);
                var results = Mapper.Map<IEnumerable<ProductDto>>(products);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route}/{productHeaderId}/products - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpGet("{productHeaderId}/products/{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int productHeaderId, [FromRoute] int id)
        {
            try
            {
                if (!await _productRepository.ProductHeaderExistsAsync(productHeaderId))
                {
                    return NotFound();
                }
                var product = await _productRepository.GetProductAsync(id);
                if (product == null)
                    return NotFound();

                var result = Mapper.Map<ProductDto>(product);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route}/{productHeaderId}/products/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpPost("{productHeaderId}/products")]
        public async Task<IActionResult> AddProduct([FromRoute] int productHeaderId, [FromBody] NewProductDto newProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _productRepository.ProductHeaderExistsAsync(productHeaderId))
                {
                    return NotFound();
                }

                if (await _productRepository.IsDuplicateProductAsync(productHeaderId, newProduct))
                {
                    ModelState.AddModelError("product", "Product already exists");
                    return BadRequest(ModelState);
                }

                var productId = await _productRepository.AddProductAsync(productHeaderId, newProduct);

                if (productId > 0)
                {
                    return Ok(productId);
                }

                return StatusCode(500, "An error ocurred in server");
            }
            catch (Exception e)
            {
                _logger.LogCritical($"POST {Route}/{productHeaderId}/products - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpPut("{productHeaderId}/products")]
        public async Task<IActionResult> EditProduct([FromRoute] int productHeaderId, [FromBody] EditProductDto product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _productRepository.ProductHeaderExistsAsync(productHeaderId))
                {
                    return NotFound();
                }

                if (!await _productRepository.ProductExistsAsync(productHeaderId, product.Id))
                {
                    ModelState.AddModelError("product", "Product not found");
                    return BadRequest(ModelState);
                }

                if (await _productRepository.IsDuplicateProductAsync(productHeaderId, product))
                {
                    ModelState.AddModelError("product", "Product already exists");
                    return BadRequest(ModelState);
                }

                var wasProductEdited = await _productRepository.EditProductAsync(product);

                if (wasProductEdited)
                {
                    return Ok();
                }

                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogCritical($"PUT {Route}/{productHeaderId}/products - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpDelete("{productHeaderId}/products/{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int productHeaderId, [FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _productRepository.ProductHeaderExistsAsync(productHeaderId))
                {
                    return NotFound();
                }

                if (!await _productRepository.ProductExistsAsync(productHeaderId, id))
                {
                    return NotFound();
                }

                var wasProductDeleted = await _productRepository.DeleteProductAsync(id);

                if (wasProductDeleted)
                {
                    return Ok();
                }

                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogCritical($"DELETE {Route}/{productHeaderId}/products/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }
    }
}