using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name {get; set;} //Property with getter and setter methods
        public string Description {get; set;}
        public decimal Price {get; set;}
        public string PictureUrl {get; set;}
        public ProductType ProductType {get; set;}
        public int ProductTypeId {get; set;}
        public ProductBrand ProductBrand {get; set;} //Relative entity
        public int ProductBrandId {get; set;}

    }
}