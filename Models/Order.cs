using System;
using System.Collections.Generic;

namespace Wahama
{
    public partial class Order
    {
        public int Id { get; set; }        
        public int UserId { get; set; } // Для сопоставления с корзиной
        public int CheckId { get; set; }               
        public Check Check { get; set; }
       
    }
}
