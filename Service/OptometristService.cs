using System.Globalization;
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


    public async Task<ReportViewModel> GenerateReports()
    {
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
                TotalAmount = booking.TotalAmount,
                PaymentStatus = payment.IsPaid,
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
        var bookingHistory = await query.OrderByDescending(o=>o.BookingId).ToListAsync();
        var dailyBookings = await _context.Bookings.Where( day => day.Date.Date == DateTime.Today).ToListAsync();
        var currentDate = DateTime.Now;
        var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        var monthlyBookings = await _context.Bookings
            .Where(booking => booking.Date.Date >= firstDayOfMonth && booking.Date.Date <= lastDayOfMonth)
            .ToListAsync();
        var totalMonthlyRevenue = monthlyBookings.Sum(booking => booking.TotalAmount);
        var totalBookings = await _context.Bookings.CountAsync();
        return new ReportViewModel()
        {
            DailyBooking = dailyBookings.Count,
            MonthlyBooking = monthlyBookings.Count,
            TotalMonthlyRevenue = totalMonthlyRevenue,
            Bookings = bookingHistory,
            AllBookings =  totalBookings,
            DailyBookings = await query.Where(d => d.Date.Date == DateTime.Today).ToListAsync(),
            TotalClient = await _context.Patients.CountAsync(),
            Prescriptions = await prescriptionQuery.ToListAsync(),
            Patients = await _context.Patients.Select(p => new PatientViewModel()
            {
                Name = p.Name,
                PatientId = p.PatientId,
                Surname = p.Surname,
                Address = p.Address,
                Email = p.Email,
                Cellphone = p.Cellphone,
                DoB = p.DoB
            }).ToListAsync()
        };
    }
}