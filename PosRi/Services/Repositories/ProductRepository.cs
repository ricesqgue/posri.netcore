using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PosRi.Entities;
using PosRi.Entities.Context;
using PosRi.Models.Request.Product;
using PosRi.Services.Contracts;

namespace PosRi.Services.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly PosRiContext _dbContext;

        public ProductRepository(PosRiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductHeader> GetProductHeaderAsync(int id)
        {
            return await _dbContext.ProductHeaders
                .Include(p => p.Products.Select(c => c.ColorPrimary))
                .Include(p => p.Products.Select(c => c.ColorSecondary))
                .Include(p => p.Products.Select(c => c.Size))
                .Include(p => p.Brand)
                .Include(p => p.SubCategory)
                .Select(p => new ProductHeader()
                {
                    Id = p.Id,
                    Model = p.Model,
                    Description = p.Description,
                    ShortDescription = p.ShortDescription,
                    SubCategory = p.SubCategory,
                    Brand = p.Brand,                 
                    IsActive = p.IsActive,
                    Products = p.Products.Where(s => s.IsActive).ToList()
                })
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<ICollection<ProductHeader>> GetProductHeadersAsync()
        {
            return await _dbContext.ProductHeaders
                .Include(p => p.Products)
                .Include(p => p.Brand)
                .Include(p => p.SubCategory)
                .Where(p => p.IsActive)
                .Select(p => new ProductHeader()
                {
                    Id = p.Id,
                    Model = p.Model,
                    Description = p.Description,
                    ShortDescription = p.ShortDescription,
                    SubCategory = p.SubCategory,
                    Brand = p.Brand,
                    IsActive = p.IsActive,
                    Products = p.Products.Where(s => s.IsActive).ToList()
                })
                .ToListAsync();
        }

        public async Task<bool> IsDuplicateProductHeaderAsync(NewProductHeaderDto productHeader)
        {
            //Check header
            var duplicate =  await _dbContext.ProductHeaders.AnyAsync(c =>
                c.Model.Equals(productHeader.Model, StringComparison.InvariantCultureIgnoreCase));

            if (duplicate) return true;

            var products = productHeader.Products.ToList();

            //Check duplicates in product list
            for (int i = 0; i < products.Count; i++)
            {
                for (int j = i + 1; j < products.Count; j++)
                {
                    if (products[i].Code.Equals(products[j].Code, StringComparison.InvariantCultureIgnoreCase) 
                        //|| (products[i].Size.Id == products[j].Size.Id
                        //&& products[i].ColorPrimary.Id == products[j].ColorPrimary.Id
                        //&& products[i].ColorSecondary.Id == products[j].ColorSecondary.Id
                        //&& products[i].ExtraDescription.Equals(products[j].ExtraDescription, StringComparison.InvariantCultureIgnoreCase))
                        )
                    {
                        return true;
                    }
                }
            }

            //Check for duplicates codes in db
            var productCodes = products.Select(p => p.Code).ToList();
            return await _dbContext.Products.AnyAsync(p => productCodes.Contains(p.Code));
        }

        public async Task<bool> IsDuplicateProductHeaderAsync(EditProductHeaderDto productHeader)
        {
            return await _dbContext.ProductHeaders.AnyAsync(c =>
                c.Model.Equals(productHeader.Model, StringComparison.InvariantCultureIgnoreCase) && c.Id != productHeader.Id);
        }

        public async Task<bool> ProductHeaderExistsAsync(int id)
        {
            return await _dbContext.ProductHeaders.AnyAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<int> AddProductHeaderAsync(NewProductHeaderDto newProductHeader)
        {
            var productHeader = new ProductHeader
            {
                Model = newProductHeader.Model,
                Description = newProductHeader.Description,
                ShortDescription = newProductHeader.ShortDescription,
                SubCategoryId = newProductHeader.SubCategory.Id,
                BrandId = newProductHeader.Brand.Id,
                IsActive = true
            };

            var products = new List<Product>();
            foreach (var productDto in newProductHeader.Products)
            {
                products.Add(new Product
                {
                    Code = productDto.Code,
                    ExtraDescription = productDto.ExtraDescription,
                    Price = productDto.Price,
                    SpecialPrice = productDto.SpecialPrice,
                    CreateDate = DateTime.Now,
                    SizeId = productDto.Size.Id,
                    ColorPrimaryId = productDto.ColorPrimary.Id,
                    ColorSecondaryId = productDto.ColorSecondary.Id,
                    IsActive = true
                });
            }

            productHeader.Products = products;

            await _dbContext.ProductHeaders.AddAsync(productHeader);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return productHeader.Id;
            }

            return 0;
        }

        public async Task<bool> EditProductHeaderAsync(EditProductHeaderDto editProductHeader)
        {
            var productHeader = await _dbContext.ProductHeaders.FindAsync(editProductHeader.Id);

            productHeader.Model = editProductHeader.Model;
            productHeader.Description = editProductHeader.Description;
            productHeader.ShortDescription = editProductHeader.ShortDescription;
            productHeader.SubCategoryId = editProductHeader.SubCategory.Id;
            productHeader.BrandId = editProductHeader.Brand.Id;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductHeaderAsync(int id)
        {
            var productHeader = await _dbContext.ProductHeaders
                .Include(p => p.Products)
                .FirstOrDefaultAsync(p => p.Id == id);

            productHeader.IsActive = false;

            var products = productHeader.Products.ToList();

            foreach (var product in products)
            {
                product.IsActive = false;
            }

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _dbContext.Products
                    .Include(p => p.Size)
                    .Include(p => p.ColorPrimary)
                    .Include(p => p.ColorSecondary)
                .FirstOrDefaultAsync(s => s.Id == id && s.IsActive);
        }

        public async Task<ICollection<Product>> GetProductsAsync(int productHeaderId)
        {
            return await _dbContext.Products
                .Include(p => p.Size)
                .Include(p => p.ColorPrimary)
                .Include(p => p.ColorSecondary)
                .Where(s => s.ProductHeaderId == productHeaderId && s.IsActive)
                .ToListAsync();
        }

        public async Task<bool> IsDuplicateProductAsync(int productHeaderId, NewProductDto product)
        {
            return await _dbContext.Products.AnyAsync(c =>
                c.Code.Equals(product.Code, StringComparison.InvariantCultureIgnoreCase) && c.IsActive);
        }

        public async Task<bool> IsDuplicateProductAsync(int productHeaderId, EditProductDto product)
        {
            return await _dbContext.Products.AnyAsync(c =>
                c.Code.Equals(product.Code, StringComparison.InvariantCultureIgnoreCase) && c.Id != product.Id);
        }

        public async Task<bool> ProductExistsAsync(int productHeaderId, int id)
        {
            return await _dbContext.Products.AnyAsync(s => s.Id == id && s.ProductHeaderId == productHeaderId && s.IsActive);
        }

        public async Task<int> AddProductAsync(int productHeaderId, NewProductDto newProduct)
        {
            var product = new Product()
            {
                Code = newProduct.Code,
                ExtraDescription = newProduct.ExtraDescription,
                Price = newProduct.Price,
                SpecialPrice = newProduct.SpecialPrice,
                CreateDate = DateTime.Now,
                SizeId = newProduct.Size.Id,
                ColorPrimaryId = newProduct.ColorPrimary.Id,
                ColorSecondaryId = newProduct.ColorSecondary.Id,
                ProductHeaderId = productHeaderId,
                IsActive = true                
            };

            await _dbContext.Products.AddAsync(product);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return product.Id;
            }

            return 0;
        }

        public async Task<bool> EditProductAsync(EditProductDto product)
        {
            var productEdit = await _dbContext.Products.FindAsync(product.Id);

            productEdit.Code = product.Code;
            productEdit.ExtraDescription = product.ExtraDescription;
            productEdit.Price = product.Price;
            productEdit.SpecialPrice = product.SpecialPrice;
            productEdit.SizeId = product.Size.Id;
            productEdit.ColorPrimaryId = product.ColorPrimary.Id;
            productEdit.ColorSecondaryId = product.ColorSecondary.Id;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);

            product.IsActive = false;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
