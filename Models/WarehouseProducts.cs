using System;
using System.Collections.Generic;

namespace Wahama
{
    public partial class WarehouseProducts
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }
        public int PurchasePrice { get; set; }
        public int WarehouseId { get; set; }

        public Product Product { get; set; }
        public WarehouseList Warehouse { get; set; }
    }
}
