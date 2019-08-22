using System;
using System.Collections.Generic;

namespace Wahama
{
    public partial class ProductSetting
    {
        public ProductSetting()
        {
            ProductAlliance = new HashSet<ProductAlliance>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public string ImageSource { get; set; }
        public ICollection<ProductAlliance> ProductAlliance { get; set; }
    }
}
