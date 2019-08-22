
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using Wahama.Models;

namespace Wahama.Models
{
    public class CartItemViewModel
    {
        public Dictionary<int, string> ImageSources { get; set; }
        public IEnumerable<ShoppingCart> CartItem { get; set; }
        public IIncludableQueryable<Product, ProductType> Products { get; set; }
    }
}
