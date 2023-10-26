using Microsoft.EntityFrameworkCore;
using OptiApp.Models;
using OptiApp.ViewModel;

namespace OptiApp.Service;

public class BookingService
{
    private readonly ApplicationDbContext _context;

    public BookingService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    private async Task<bool> UpdateSlot(int id)
    {
        var slot =  await _context.TimeSlots.FirstOrDefaultAsync(slot => slot.Id.Equals(id));
        if (slot is null)
        {
            return false;
        }

        slot.IsTaken = true;

        _context.Update(slot);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
    private async Task<bool> CancelSlot(int id)
    {
        var slot =  await _context.TimeSlots.FirstOrDefaultAsync(slot => slot.Id.Equals(id));
        if (slot is null)
        {
            return false;
        }

        slot.IsTaken = false;

        _context.Update(slot);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
    public async Task<bool> Book(BookingViewModel booking)
    {
        
        var updated = await UpdateSlot(booking.TimeSlotId!.Value);
        if (!updated) return false;
        var bookingModel = new Booking()
        {
            PatientId = booking.PatientId,
            Services = booking.Services!,
            Status = "Unpaid",
            Date = booking.Date!,
            Slot = "",
            TimeSlotId = booking.TimeSlotId,
            TotalAmount = booking.TotalAmount
        };
        await _context.Bookings.AddAsync(bookingModel);
        await _context.SaveChangesAsync();
        
        
        var payment = new Payment()
        {
            BookingId = bookingModel.BookingId,
            AmountPaid = bookingModel.TotalAmount,
            Change = 0
        };
        
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
        return true;
    }
}