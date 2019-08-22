using System;
using System.Collections.Generic;

namespace Wahama
{
    public partial class WarehouseList
    {
        public WarehouseList()
        {
            WarehouseProducts = new HashSet<WarehouseProducts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public ICollection<WarehouseProducts> WarehouseProducts { get; set; }
    }
}
