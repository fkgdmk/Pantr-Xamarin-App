using System;
using System.Collections.Generic;
using System.Text;

namespace Pantr.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Material { get; set; }
        public UserViewModel Giver { get; set; }
        public string PostQuantity { get; set; }
        public string Address { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool Claimed { get; set; }
        public bool Completed { get; set; }
        public string Date { get; set; }
    }
}
