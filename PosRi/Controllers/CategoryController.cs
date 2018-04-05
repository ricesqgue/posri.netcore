using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PosRi.Models.Request.Category;
using PosRi.Models.Response;
using PosRi.Services.Contracts;

namespace PosRi.Controllers
{
    [Produces("application/json")]
    [Route("api/category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryController> _logger;

        private const string Route = "api/category";

        public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetCategoriesAsync();
                var results = Mapper.Map<IEnumerable<CategoryDto>>(categories);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryAsync(id);
                if (category == null)
                    return NotFound();

                var result = Mapper.Map<CategoryDto>(category);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route}/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] NewCategoryDto newCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _categoryRepository.IsDuplicateCategoryAsync(newCategory))
                {
                    ModelState.AddModelError("category", "Category already exists");
                    return BadRequest(ModelState);
                }

                var categoryId = await _categoryRepository.AddCategoryAsync(newCategory);

                if (categoryId > 0)
                {
                    return Ok(categoryId);
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
        public async Task<IActionResult> EditCategory([FromBody] EditCategoryDto category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _categoryRepository.CategoryExistsAsync(category.Id))
                {
                    ModelState.AddModelError("category", "Category not found");
                    return BadRequest(ModelState);
                }

                if (await _categoryRepository.IsDuplicateCategoryAsync(category))
                {
                    ModelState.AddModelError("category", "Category already exists");
                    return BadRequest(ModelState);
                }

                var wasCategoryEdited = await _categoryRepository.EditCategoryAsync(category);

                if (wasCategoryEdited)
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
        public async Task<IActionResult> DeleteCategory([FromRoute]int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _categoryRepository.CategoryExistsAsync(id))
                {
                    return NotFound();
                }

                var wasCategoryDeleted = await _categoryRepository.DeleteCategoryAsync(id);

                if (wasCategoryDeleted)
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

        [HttpGet("{categoryId}/subcategories")]
        public async Task<IActionResult> GetSubCategories([FromRoute] int categoryId)
        {
            try
            {
                if (!await _categoryRepository.CategoryExistsAsync(categoryId))
                {
                    return NotFound();
                }
                var subCategories = await _categoryRepository.GetSubCategoriesAsync(categoryId);
                var results = Mapper.Map<IEnumerable<SubCategoryDto>>(subCategories);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route}/{categoryId}/subcategories - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpGet("{categoryId}/subcategories/{id}")]
        public async Task<IActionResult> GetSubCategory([FromRoute] int categoryId, [FromRoute] int id)
        {
            try
            {
                if (!await _categoryRepository.CategoryExistsAsync(categoryId))
                {
                    return NotFound();
                }
                var subCategory = await _categoryRepository.GetSubCategoryAsync(id);
                if (subCategory == null)
                    return NotFound();

                var result = Mapper.Map<SubCategoryDto>(subCategory);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"GET {Route}/{categoryId}/subcategories/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpPost("{categoryId}/subcategories")]
        public async Task<IActionResult> AddSubCategory([FromRoute] int categoryId, [FromBody] NewSubCategoryDto newSubCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _categoryRepository.CategoryExistsAsync(categoryId))
                {
                    return NotFound();
                }

                if (await _categoryRepository.IsDuplicateSubCategoryAsync(categoryId, newSubCategory))
                {
                    ModelState.AddModelError("subCategory", "Subcategory already exists");
                    return BadRequest(ModelState);
                }

                var subCategoryId = await _categoryRepository.AddSubCategoryAsync(categoryId, newSubCategory);

                if (subCategoryId > 0)
                {
                    return Ok(subCategoryId);
                }

                return StatusCode(500, "An error ocurred in server");
            }
            catch (Exception e)
            {
                _logger.LogCritical($"POST {Route}/{categoryId}/subcategories - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpPut("{categoryId}/subcategories")]
        public async Task<IActionResult> EditSubCategory([FromRoute] int categoryId, [FromBody] EditSubCategoryDto subCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _categoryRepository.CategoryExistsAsync(categoryId))
                {
                    return NotFound();
                }

                if (!await _categoryRepository.SubCategoryExistsAsync(categoryId, subCategory.Id))
                {
                    ModelState.AddModelError("subCategory", "Sub category not found");
                    return BadRequest(ModelState);
                }

                if (await _categoryRepository.IsDuplicateSubCategoryAsync(categoryId, subCategory))
                {
                    ModelState.AddModelError("subCategory", "Sub category already exists");
                    return BadRequest(ModelState);
                }

                var wasSubCategoryEdited = await _categoryRepository.EditSubCategoryAsync(subCategory);

                if (wasSubCategoryEdited)
                {
                    return Ok();
                }

                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogCritical($"PUT {Route}/{categoryId}/subcategories - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }

        [HttpDelete("{categoryId}/subcategories/{id}")]
        public async Task<IActionResult> DeleteSubCategory([FromRoute] int categoryId, [FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _categoryRepository.CategoryExistsAsync(categoryId))
                {
                    return NotFound();
                }

                if (!await _categoryRepository.SubCategoryExistsAsync(categoryId, id))
                {
                    return NotFound();
                }

                var wasSubCategoryDeleted = await _categoryRepository.DeleteSubCategoryAsync(id);

                if (wasSubCategoryDeleted)
                {
                    return Ok();
                }

                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogCritical($"DELETE {Route}/{categoryId}/subcategories/{id} - {e.GetType().Name} - {e.Message} - {e.StackTrace}");
                return StatusCode(500, "An error ocurred in server");
            }
        }
    }
}