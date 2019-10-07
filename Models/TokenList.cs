using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wahama.Models
{
    public class TokenList
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Login { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
