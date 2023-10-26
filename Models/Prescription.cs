using System;
using System.Collections.Generic;

namespace OptiApp.Models
{
    public partial class Prescription
    {
        public int PrescriptionId { get; set; }
        public int BookingId { get; set; }
        public string? PrescriptionNote { get; set; }
    }
}
