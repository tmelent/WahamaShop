using System;
using System.Collections.Generic;

namespace Wahama
{
    public partial class Order
    {
        public int Id { get; set; }        
        public int UserId { get; set; } // Для поиска товаров в корзине 
        public int CheckId { get; set; }        
       
        public Check Check { get; set; }
       
    }
}
