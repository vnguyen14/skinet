using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}