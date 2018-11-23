using System;
using System.Collections.Generic;
using System.Text;

namespace Pantr.DB
{
    public class CityObj
    {
        public int Zip { get; set; }
        public string City { get; set; }
    }

    public class AddressObj
    {
        public string Address { get; set; }
        public CityObj City { get; set; }
    }

    class User
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsPanter { get; set; }
        public AddressObj Address { get; set; }
    }
}
