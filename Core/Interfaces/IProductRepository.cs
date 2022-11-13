using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        //The signature of 2 methods. Both are async methods, so they're gonna return a task
        Task<Product> GetProductByIdAsync(int id); //GetProductByIdAsync is just a name, the async part is the naming convention that indicates the method will return a task
        Task<IReadOnlyList<Product>> GetProductsAsync(); //IReadOnlyList is a list type that doesn't allow the regular list to be modified
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        
    }
}