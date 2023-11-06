using System.ComponentModel.DataAnnotations;

namespace OptiApp.ViewModel;

public class BookingHistoryViewModel
{
    public int BookingId { get; set; }
    public DateTime Date { get; set; }
    public string? Services { get; set; }
    public decimal TotalAmount { get; set; }
    public string? TimeSlot { get; set; }
    public bool PaymentStatus { get; set; }
    public string? PrescriptionNote { get; set; }
}