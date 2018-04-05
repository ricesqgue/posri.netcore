using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PosRi.Entities;
using PosRi.Entities.Context;
using PosRi.Models.Request.Category;
using PosRi.Services.Contracts;

namespace PosRi.Services.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly PosRiContext _dbContext;

        public CategoryRepository(PosRiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await _dbContext.Categories
                .Include(c => c.SubCategories)
                .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsActive = c.IsActive,
                    SubCategories = c.SubCategories.Where(s => s.IsActive).ToList()
                })
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            return await _dbContext.Categories
                .Include(c => c.SubCategories)
                .Where(c => c.IsActive)
                .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsActive = c.IsActive,
                    SubCategories = c.SubCategories.Where(s => s.IsActive).ToList()
                })
                .ToListAsync();
        }

        public async Task<bool> IsDuplicateCategoryAsync(NewCategoryDto category)
        {
            return await _dbContext.Categories.AnyAsync(c =>
                c.Name.Equals(category.Name, StringComparison.InvariantCultureIgnoreCase) && c.IsActive);
        }

        public async Task<bool> IsDuplicateCategoryAsync(EditCategoryDto category)
        {
            return await _dbContext.Categories.AnyAsync(c =>
                c.Name.Equals(category.Name, StringComparison.InvariantCultureIgnoreCase) && c.Id != category.Id && c.IsActive);
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _dbContext.Categories.AnyAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<int> AddCategoryAsync(NewCategoryDto newCategory)
        {
            var category = new Category
            {
                Name = newCategory.Name,
                IsActive = true
            };

            await _dbContext.Categories.AddAsync(category);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return category.Id;
            }

            return 0;
        }

        public async Task<bool> EditCategoryAsync(EditCategoryDto editCategory)
        {
            var category = await _dbContext.Categories.FindAsync(editCategory.Id);

            category.Name = editCategory.Name;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);

            category.IsActive = false;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<SubCategory> GetSubCategoryAsync(int id)
        {
            return await _dbContext.SubCategories.FirstOrDefaultAsync(s => s.Id == id && s.IsActive);
        }

        public async Task<ICollection<SubCategory>> GetSubCategoriesAsync(int categoryId)
        {
            return await _dbContext.SubCategories.Where(s => s.CategoryId == categoryId && s.IsActive).ToListAsync();
        }

        public async Task<bool> IsDuplicateSubCategoryAsync(int categoryId, NewSubCategoryDto subCategory)
        {
            return await _dbContext.SubCategories.AnyAsync(c =>
                c.Name.Equals(subCategory.Name, StringComparison.InvariantCultureIgnoreCase) && c.IsActive && c.CategoryId == categoryId);
        }

        public async Task<bool> IsDuplicateSubCategoryAsync(int categoryId, EditSubCategoryDto subCategory)
        {
            return await _dbContext.SubCategories.AnyAsync(c =>
                c.Name.Equals(subCategory.Name, StringComparison.InvariantCultureIgnoreCase) && c.Id != subCategory.Id && c.IsActive && c.CategoryId == categoryId);
        }

        public async Task<bool> SubCategoryExistsAsync(int categoryId, int id)
        {
            return await _dbContext.SubCategories.AnyAsync(s => s.Id == id && s.CategoryId == categoryId && s.IsActive);
        }

        public async Task<int> AddSubCategoryAsync(int categoryId, NewSubCategoryDto newSubCategory)
        {
            var subCategory = new SubCategory()
            {
                Name = newSubCategory.Name,
                IsActive = true,
                CategoryId = categoryId
            };

            await _dbContext.SubCategories.AddAsync(subCategory);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return subCategory.Id;
            }

            return 0;
        }

        public async Task<bool> EditSubCategoryAsync(EditSubCategoryDto subCategory)
        {
            var subCategoryEdit = await _dbContext.SubCategories.FindAsync(subCategory.Id);

            subCategoryEdit.Name = subCategory.Name;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteSubCategoryAsync(int id)
        {
            var subCategory = await _dbContext.SubCategories.FindAsync(id);

            subCategory.IsActive = false;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
