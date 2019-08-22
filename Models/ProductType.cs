using System;
using System.Collections.Generic;

namespace Wahama
{
    public partial class ProductType
    {
        public ProductType()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
