using System;
using System.Collections.Generic;

namespace Wahama
{
    public partial class ShoppingCart
    {
        public ShoppingCart()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }

        public Product Product { get; set; }
        public ICollection<Order> Order { get; set; }
    }
}
