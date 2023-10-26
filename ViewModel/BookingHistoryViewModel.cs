namespace OptiApp.ViewModel;

public class BookingHistoryViewModel
{
    public int BookingId { get; set; }
    public string? Date { get; set; }
    public string? Services { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Status { get; set; }
    public string? TimeSlot { get; set; }
}