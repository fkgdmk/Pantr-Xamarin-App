using System;
using System.Collections.Generic;
using System.Text;

namespace Pantr.Models
{
    public class PostViewModelCopy
    {
        public string Material { get; set; }
        public string Quantity { get; set; }
        public int Bags { get; set; }
        public int Sacks { get; set; }
        public int Cases { get; set; }
        public string Address { get; set; }
        public string PeriodForPickup { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool Claimed { get; set; }
        public bool Completed { get; set; }
        public int Id { get; set; }
        public string DateAndPeriod { get; set; }
    }

}
