using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wahama.Models
{
    public class ProductsViewModel
    {
        public Dictionary<int, string> ImageSources { get; set; }
        public IIncludableQueryable<Product, ProductType> Products { get; set; }
    }
}
