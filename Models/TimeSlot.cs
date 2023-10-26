using System;
using System.Collections.Generic;

namespace OptiApp.Models
{
    public partial class TimeSlot
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool? IsTaken { get; set; }
    }
}
