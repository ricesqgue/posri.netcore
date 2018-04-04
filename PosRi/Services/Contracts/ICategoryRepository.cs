using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;
using PosRi.Models.Request.Category;

namespace PosRi.Services.Contracts
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryAsync(int id);

        Task<ICollection<Category>> GetCategoriesAsync();

        Task<bool> IsDuplicateCategoryAsync(NewCategoryDto category);

        Task<bool> IsDuplicateCategoryAsync(EditCategoryDto category);

        Task<bool> CategoryExistsAsync(int id);

        Task<int> AddCategoryAsync(NewCategoryDto newCategory);

        Task<bool> EditCategoryAsync(EditCategoryDto category);

        Task<bool> DeleteCategoryAsync(int id);

        Task<SubCategory> GetSubCategoryAsync(int id);

        Task<ICollection<SubCategory>> GetSubCategoriesAsync(int categoryId);

        Task<bool> IsDuplicateSubCategoryAsync(int categoryId, NewSubCategoryDto subCategory);

        Task<bool> IsDuplicateSubCategoryAsync(int categoryId, EditSubCategoryDto subCategory);

        Task<bool> SubCategoryExistsAsync(int id);

        Task<int> AddSubCategoryAsync(int categoryId, NewSubCategoryDto newSubCategory);

        Task<bool> EditSubCategoryAsync(EditSubCategoryDto subCategory);

        Task<bool> DeleteSubCategoryAsync(int id);

    }
}
