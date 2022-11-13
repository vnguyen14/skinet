using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrand.ToListAsync();
        }

        //What we do in the methods are pretty much the same as in the Controllers
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                //Add .Include to get product types and brands data when request data for products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                //Change .FindAsync() to FirstOrDefaultAsync() or SingleOrDefaultAsync() to fix the issue 'IIncludableQueryable<Product, ProductBrand>' does not contain a definition for 'FindAsync''
                //.FindAsync(id);
                .FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products
                //Add .Include to get product types and brands data when request data for products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductType.ToListAsync();
        }
    }
}