using System;
using System.Collections.Generic;

namespace OptiApp.Models
{
    public partial class Service
    {
        public int ServiceId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
