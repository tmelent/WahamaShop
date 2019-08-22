using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Wahama
{
    public partial class Product
    {
        public Product()
        {
            ProductImage = new HashSet<ProductImage>();
            ShoppingCart = new HashSet<ShoppingCart>();
            WarehouseProducts = new HashSet<WarehouseProducts>();
        }
        
        public int Id { get; set; }
       
        public int ProductFractionId { get; set; }
        
        public int ProductTypeId { get; set; }
        [DisplayName("Наименование")]
        public string Name { get; set; }
        [DisplayName("Цена")]
        public int Price { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }
        [DisplayName("Короткое описание")]
        public string ShortDescription { get; set; }
        [DisplayName("Фракция")]
        public ProductFraction ProductFraction { get; set; }
        [DisplayName("Тип товара")]
        public ProductType ProductType { get; set; }
        public ICollection<ProductImage> ProductImage { get; set; }
        public ICollection<ShoppingCart> ShoppingCart { get; set; }
        public ICollection<WarehouseProducts> WarehouseProducts { get; set; }

        public string ImageSource { get; set; }
    }

}
