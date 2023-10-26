using Microsoft.AspNetCore.Identity;
using OptiApp.Models;

namespace OptiApp.ViewModel;

public class ProfileViewModel
{
    public Patient? User { get; set; }
    public IEnumerable<BookingHistoryViewModel>? BookingHistory { get; set; }
}