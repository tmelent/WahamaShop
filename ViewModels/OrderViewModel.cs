using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wahama.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public Dictionary<int, string> ImageSources { get; set; }
        public Customer Customer { get; set; }
        public Address Address { get; set; }
        public Check Check { get; set; }
        public IEnumerable<ShoppingCart> CartItem { get; set; }
        public IIncludableQueryable<Product, ProductType> Products { get; set; }
    }
}
