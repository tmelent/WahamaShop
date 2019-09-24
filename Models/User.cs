using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wahama.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
