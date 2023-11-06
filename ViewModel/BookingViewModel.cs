namespace OptiApp.ViewModel;

public class BookingViewModel
{
    public int PatientId { get; set; }
    public DateTime Date { get; set; } 
    public string? Services { get; set; } 
    public decimal TotalAmount { get; set; }
    public string? Status { get; set; } 
    public int? TimeSlotId { get; set; }
}