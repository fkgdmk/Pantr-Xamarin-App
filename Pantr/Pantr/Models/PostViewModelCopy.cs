using System;
using System.Collections.Generic;
using System.Text;

namespace Pantr.Models
{
    public class PostViewModelCopy
    {
        public string Material { get; set; }
        // public PostQuantity PostQuantity { get; set; }    
        public string Quantity { get; set; }
        public string Address { get; set; }
        public string PeriodForPickup { get; set; }
        public bool Claimed { get; set; }
        public bool Completed { get; set; }
        public string Date { get; set; }
    }
}
