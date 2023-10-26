using System;
using System.Collections.Generic;

namespace OptiApp.Models
{
    public partial class Receptionist
    {
        public int ReceptionistId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Cellphone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string DoB { get; set; } = null!;
        public string? Email { get; set; }
    }
}
