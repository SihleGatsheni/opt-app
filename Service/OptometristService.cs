using Microsoft.EntityFrameworkCore;
using OptiApp.Models;
using OptiApp.ViewModel;

namespace OptiApp.Service;

public class OptometristService
{
    private readonly ApplicationDbContext _context;

    public OptometristService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<ReportViewModel> GenerateReports(HttpContext context)
    {
        var patient =
            await _context.Optometrists.SingleOrDefaultAsync(u => u.Email.Equals(context.User.Identity!.Name));

        var query = from booking in _context.Bookings
            join payment in _context.Payments on booking.BookingId equals payment.BookingId
            select new BookingHistoryViewModel
            {
                BookingId = booking.BookingId,
                Date = booking.Date,
                TimeSlot = booking.Slot != "" ? booking.Slot: _context.TimeSlots.
                    SingleOrDefault(o=>o.Id.Equals(booking.TimeSlotId)).StartTime + "-" + _context.TimeSlots.
                    SingleOrDefault(o=>o.Id.Equals(booking.TimeSlotId)).EndTime,
                Services = booking.Services,
                Status = booking.Status,
                TotalAmount = booking.TotalAmount,
                IsPaid = payment.IsPaid,
            };

        var prescriptionQuery = from booking in _context.Bookings
            join presc in _context.Prescriptions on booking.BookingId equals presc.BookingId
            join p in _context.Patients on booking.PatientId equals p.PatientId
            select new PrescriptionViewModel()
            {
                BookingId = presc.BookingId,
                Patient = p,
                PrescriptionNote = presc.PrescriptionNote
            };
        var bookingHistory = await query.OrderByDescending(o=>o.BookingId).Take(5).ToListAsync();
        return new ReportViewModel()
        {
            Bookings = bookingHistory,
            Clients = await _context.Bookings.CountAsync(),
            Prescriptions = await prescriptionQuery.Take(5).ToListAsync()
        };
    }
}