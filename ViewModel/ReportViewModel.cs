using OptiApp.Models;

namespace OptiApp.ViewModel;

public class ReportViewModel
{
    public int TotalClient { get; set; }
    public int DailyBooking { get; set; }
    public int MonthlyBooking { get; set; }
    public int AllBookings { get; set; }
    public decimal TotalMonthlyRevenue { get; set; }
    public IEnumerable<BookingHistoryViewModel>? DailyBookings { get; set; }
    public IEnumerable<BookingHistoryViewModel>? Bookings { get; set; }
    public IEnumerable<PrescriptionViewModel>? Prescriptions { get; set; }
    public IEnumerable<PatientViewModel>? Patients { get; set; }
}