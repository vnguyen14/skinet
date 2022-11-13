using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) //constructor that provides it with some options, the options we're gonna provide is the connection strings. Then it passes those options to the base constructor (DbContext constructor)
        {
        }

        public DbSet<Product> Products {get; set;}
        public DbSet<ProductType> ProductType {get; set;}
        public DbSet<ProductBrand> ProductBrand {get; set;}
        //Overide OnModelCreating method to tell StoreContext there's configuration to look for 
        protected override void OnModelCreating(ModelBuilder modelBuilder) //specify void cause we're not returning anything
        {
            base.OnModelCreating(modelBuilder); //base is the DBContext class that we are depriving from
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //specify modelbuilder. Assembly need to be a capital A
        }
    }
}