using System;
using System.Collections.Generic;

namespace Wahama
{
    public partial class ProductFraction
    {
        public ProductFraction()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public int ProductAllianceId { get; set; }
        public string Title { get; set; }
        public string ImageSource { get; set; }
        public ProductAlliance ProductAlliance { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
