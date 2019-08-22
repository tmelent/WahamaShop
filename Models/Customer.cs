using System;
using System.Collections.Generic;

namespace Wahama
{
    public partial class Customer
    {
        public Customer()
        {
            Check = new HashSet<Check>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int AddressId { get; set; }

        public Address Address { get; set; }
        public ICollection<Check> Check { get; set; }
    }
}
