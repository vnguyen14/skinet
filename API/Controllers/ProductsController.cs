using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase //deprive from controllerbase
    {
        private readonly IProductRepository _repo;
        
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
            
        }

        /*[HttpGet]
        //Return ActionResult of type List of Products, meaning it can be some kind of Http response status (OK response: 200 or Bad Request: 400). 
        //Syncronuous request
        public ActionResult<List<Product>> GetProducts() {
            //Get products as list. ToList() is a query that will execute a SELECT query on the db,, and return the result and store it in the products variable
            var products = _context.Products.ToList();
            return Ok(products);

        }*/

        [HttpGet]
        //Asyncronuous request
        //Achieve the same thing as the sync request, except it's creating a Task that would pass the request to a delegate to handle so that it wouldn't block the thread that 
        //this is running on until the task is completed. Then we get the exact same result (products) as before
        public async Task<ActionResult<List<Product>>> GetProducts() {
            //Use await keyword in response to async, change to ToListAsync to make the method asyncronuous
            var products = await _repo.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")] //add id parameter to distinguish between 2 HttpGet calls
        public async Task<ActionResult<Product>> GetProduct(int id) {
            return await _repo.GetProductByIdAsync(id);
        }

        //Specify the root of "brands" to separate from the get products request
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            //Wraping the return await in an Ok response to avoid the 'cannot convert List to IReadOnlyList' issue
            return Ok(await _repo.GetProductBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            //Wraping the return await in an Ok response to avoid the 'cannot convert List to IReadOnlyList' issue
            return Ok(await _repo.GetProductTypesAsync());
        }

    }
}