using System;
using System.Collections.Generic;

namespace Wahama
{
    public partial class Seller
    {
        public Seller()
        {
            Check = new HashSet<Check>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Post { get; set; }
        public string Phone { get; set; }

        public ICollection<Check> Check { get; set; }
    }
}
