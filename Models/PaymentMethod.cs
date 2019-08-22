using System;
using System.Collections.Generic;

namespace Wahama
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            Check = new HashSet<Check>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<Check> Check { get; set; }
    }
}
