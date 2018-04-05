using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosRi.Entities;
using PosRi.Models.Request.Product;

namespace PosRi.Services.Contracts
{
    public interface IProductRepository
    {
        Task<ProductHeader> GetProductHeaderAsync(int id);

        Task<ICollection<ProductHeader>> GetProductHeadersAsync();

        Task<bool> IsDuplicateProductHeaderAsync(NewProductHeaderDto productHeader);

        Task<bool> IsDuplicateProductHeaderAsync(EditProductHeaderDto productHeader);

        Task<bool> ProductHeaderExistsAsync(int id);

        Task<int> AddProductHeaderAsync(NewProductHeaderDto newProductHeader);

        Task<bool> EditProductHeaderAsync(EditProductHeaderDto productHeader);

        Task<bool> DeleteProductHeaderAsync(int id);

        Task<Product> GetProductAsync(int id);

        Task<ICollection<Product>> GetProductsAsync(int productHeaderId);

        Task<bool> IsDuplicateProductAsync(int productHeaderId, NewProductDto product);

        Task<bool> IsDuplicateProductAsync(int productHeaderId, EditProductDto product);

        Task<bool> ProductExistsAsync(int productHeaderId, int id);

        Task<int> AddProductAsync(int productHeaderId, NewProductDto newProduct);

        Task<bool> EditProductAsync(EditProductDto product);

        Task<bool> DeleteProductAsync(int id);

    }
}
