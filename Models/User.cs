using System;
using System.Collections.Generic;

namespace OptiApp.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Position { get; set; } = null!;
    }
}
