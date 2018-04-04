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
        Task<Category> GetCategory(int id);

        Task<ICollection<Category>> GetCategories();

        Task<bool> IsDuplicateCategory(NewCategoryDto category);

        Task<bool> IsDuplicateCategory(EditCategoryDto category);

        Task<bool> CategoryExists(int id);

        Task<int> AddCategory(NewCategoryDto newCategory);

        Task<bool> EditCategory(EditCategoryDto category);

        Task<bool> DeleteCategory(int id);

        Task<SubCategory> GetSubCategory(int id);

        Task<ICollection<SubCategory>> GetSubCategories(int categoryId);

        Task<bool> IsDuplicateSubCategory(int categoryId, NewSubCategoryDto subCategory);

        Task<bool> IsDuplicateSubCategory(int categoryId, EditSubCategoryDto subCategory);

        Task<bool> SubCategoryExists(int id);

        Task<int> AddSubCategory(int categoryId, NewSubCategoryDto newSubCategory);

        Task<bool> EditSubCategory(EditSubCategoryDto subCategory);

        Task<bool> DeleteSubCategory(int id);

    }
}
