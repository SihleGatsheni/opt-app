using OptiApp.Models;

namespace OptiApp.ViewModel;

public class PrescriptionViewModel
{
    public int BookingId { get; set; }
    public Patient? Patient { get; set; }
    public string? PrescriptionNote { get; set; }
}