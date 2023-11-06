using System;
using System.Collections.Generic;

namespace OptiApp.Models
{
    public partial class Booking
    {
        public int BookingId { get; set; }
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
        public string Slot { get; set; } = null!;
        public string Services { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;
        public int? TimeSlotId { get; set; }
    }
}
