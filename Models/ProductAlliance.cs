using System;
using System.Collections.Generic;

namespace Wahama
{
    public partial class ProductAlliance
    {
        public ProductAlliance()
        {
            ProductFraction = new HashSet<ProductFraction>();
        }

        public int Id { get; set; }
        public int ProductSettingId { get; set; }
        public string Title { get; set; }

        public string ImageSource { get; set; }

        public ProductSetting ProductSetting { get; set; }
        public ICollection<ProductFraction> ProductFraction { get; set; }
    }
}
