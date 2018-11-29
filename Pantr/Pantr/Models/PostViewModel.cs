﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Pantr.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public MaterialViewModel Material { get; set; }
        public UserViewModel Giver { get; set; }
        public string Quantity { get; set; }    
        public string Address { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public bool Claimed { get; set; }
        public bool Completed { get; set; }
        public string Date { get; set; }
    }
}
