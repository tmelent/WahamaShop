using System;
using System.Collections.Generic;

namespace Wahama
{
    public partial class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageSource { get; set; }

        public Product Product { get; set; }
    }
}
