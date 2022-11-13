using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        //Static keyword allow us to use a method inside a class without creating a new instance of the class
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory) //Pass in an instance of SoteContext and IloggerFactory
        {
            //Since we're running the seed mothod inside the Program class, we won't have any error handling available, so we have to use a try catch block
            try 
            {
                //Check if there are any data in the table
                if(!context.ProductBrand.Any())
                {
                    //Use ReadAllText method to get json file
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json"); //Since we're running in Program which is inside API folder, we have to go up another level before going to Infrastructure
                    //Serialize what inside json into objects
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData); //Give back a lisyt of ProductBrand type data
                    //Add the items in the list to db via context
                    foreach(var item in brands) 
                    {
                        context.ProductBrand.Add(item);
                    }
                    //Save items into db
                    await context.SaveChangesAsync();
                }

                if(!context.ProductType.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json"); 
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach(var item in types) 
                    {
                        context.ProductType.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json"); 
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach(var item in products) 
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}