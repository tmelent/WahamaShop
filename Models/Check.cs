using System;
using System.Collections.Generic;

namespace Wahama
{
    public partial class Check
    {
        public Check()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public int Total { get; set; }
        public int PaymentMethodId { get; set; }
        public bool IsPaid { get; set; }
        public Customer Customer { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
       
        public ICollection<Order> Order { get; set; }
    }
}
