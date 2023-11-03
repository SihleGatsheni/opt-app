namespace OptiApp.ViewModel;

public class ReportViewModel
{
    public int Clients { get; set; }
    public IEnumerable<BookingHistoryViewModel>? Bookings { get; set; }
    public IEnumerable<PrescriptionViewModel>? Prescriptions { get; set; }
}