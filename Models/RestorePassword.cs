using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wahama.Models
{
    public class RestorePassword
    {
        public string Email {get; set;}
        public string NewPassword { get; set; }
        public string NewPasswordConfirmation { get; set; }
    }
}
