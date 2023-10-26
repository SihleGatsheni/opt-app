using System;
using System.Collections.Generic;

namespace OptiApp.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Change { get; set; }
    }
}
