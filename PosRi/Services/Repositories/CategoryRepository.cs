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

        public async Task<Category> GetCategory(int id)
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

        public async Task<ICollection<Category>> GetCategories()
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

        public async Task<bool> IsDuplicateCategory(NewCategoryDto category)
        {
            return await _dbContext.Categories.AnyAsync(c =>
                c.Name.Equals(category.Name, StringComparison.InvariantCultureIgnoreCase) && c.IsActive);
        }

        public async Task<bool> IsDuplicateCategory(EditCategoryDto category)
        {
            return await _dbContext.Categories.AnyAsync(c =>
                c.Name.Equals(category.Name, StringComparison.InvariantCultureIgnoreCase) && c.Id != category.Id && c.IsActive);
        }

        public async Task<bool> CategoryExists(int id)
        {
            return await _dbContext.Categories.AnyAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<int> AddCategory(NewCategoryDto newCategory)
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

        public async Task<bool> EditCategory(EditCategoryDto editCategory)
        {
            var category = await _dbContext.Categories.FindAsync(editCategory.Id);

            category.Name = editCategory.Name;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);

            category.IsActive = false;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<SubCategory> GetSubCategory(int id)
        {
            return await _dbContext.SubCategories.FirstOrDefaultAsync(s => s.Id == id && s.IsActive);
        }

        public async Task<ICollection<SubCategory>> GetSubCategories(int categoryId)
        {
            return await _dbContext.SubCategories.Where(s => s.CategoryId == categoryId && s.IsActive).ToListAsync();
        }

        public async Task<bool> IsDuplicateSubCategory(int categoryId, NewSubCategoryDto subCategory)
        {
            return await _dbContext.SubCategories.AnyAsync(c =>
                c.Name.Equals(subCategory.Name, StringComparison.InvariantCultureIgnoreCase) && c.IsActive && c.CategoryId == categoryId);
        }

        public async Task<bool> IsDuplicateSubCategory(int categoryId, EditSubCategoryDto subCategory)
        {
            return await _dbContext.SubCategories.AnyAsync(c =>
                c.Name.Equals(subCategory.Name, StringComparison.InvariantCultureIgnoreCase) && c.Id != subCategory.Id && c.IsActive && c.CategoryId == categoryId);
        }

        public async Task<bool> SubCategoryExists(int id)
        {
            return await _dbContext.SubCategories.AnyAsync(s => s.Id == id && s.IsActive);
        }

        public async Task<int> AddSubCategory(int categoryId, NewSubCategoryDto newSubCategory)
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

        public async Task<bool> EditSubCategory(EditSubCategoryDto subCategory)
        {
            var subCategoryEdit = await _dbContext.SubCategories.FindAsync(subCategory.Id);

            subCategoryEdit.Name = subCategory.Name;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteSubCategory(int id)
        {
            var subCategory = await _dbContext.SubCategories.FindAsync(id);

            subCategory.IsActive = false;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
