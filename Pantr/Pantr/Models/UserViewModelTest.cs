using System;
using System.Collections.Generic;
using System.Text;

namespace Pantr.Models
{
    public class UserViewModelTest
    {
        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsPanter { get; set; }
        public AddressViewModel Address { get; set; }
        public LoginViewModel Login { get; set; }
    }
}
