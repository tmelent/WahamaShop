using System;
using System.Collections.Generic;

namespace Wahama
{
    public partial class Address
    {
        public Address()
        {
            Customer = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string City { get; set; }
        public string Station { get; set; }
        public string Flat { get; set; }
        public int Floor { get; set; }
        public int Zip { get; set; }
        public string Country { get; set; }

        public ICollection<Customer> Customer { get; set; }
    }
}
