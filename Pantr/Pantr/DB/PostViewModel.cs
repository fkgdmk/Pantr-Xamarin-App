using System;
using System.Collections.Generic;
using System.Text;

namespace Pantr.DB
{
    class PostViewModel
    {
        public class Material
        {
            public string Type { get; set; }
        }

        public class PostView
        {
            public int Id { get; set; }
            public Material Material { get; set; }
            public object Giver { get; set; }
            public object PostQuantity { get; set; }
            public string Address { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public bool Claimed { get; set; }
            public bool Completed { get; set; }
            public string Date { get; set; }
        }
    }
}
