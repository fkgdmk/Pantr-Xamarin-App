using System;
using System.Collections.Generic;
using System.Text;

namespace Pantr.Models
{
    public class UserViewModel
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsPanter { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
