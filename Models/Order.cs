using System;
using System.Collections.Generic;

namespace Wahama
{
    public partial class Order
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public int CheckId { get; set; }

        public Check Check { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
