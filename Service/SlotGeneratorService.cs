using Microsoft.EntityFrameworkCore;
using OptiApp.Models;

namespace OptiApp.Service;

public class SlotGeneratorService
{
    private readonly ApplicationDbContext _context;

    public SlotGeneratorService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<TimeSlot>> GenerateTimeSlots(DateTime startDate, DateTime endDate)
    {
        List<TimeSlot> timeSlots = new List<TimeSlot>();
        TimeSpan slotDuration = TimeSpan.FromMinutes(60); // 

        while (startDate <= endDate)
        {
            // Check if the current day is not a weekend (Saturday or Sunday)
            if (startDate.DayOfWeek != DayOfWeek.Saturday && startDate.DayOfWeek != DayOfWeek.Sunday)
            {
                // Check the database to see if slots for this date already exist
                var existingSlots =await _context.TimeSlots
                    .Where(slot => slot.Date == startDate.Date)
                    .ToListAsync();

                if (existingSlots.Any())
                {
                    // Use existing slots from the database
                    timeSlots.AddRange(existingSlots);
                }
                else
                {
                    DateTime currentTime = startDate.Date.AddHours(8); // Start at 8 AM

                    while (currentTime.Add(slotDuration) <= startDate.Date.AddHours(16)) // End at 4 PM
                    {
                            var timeSlot = new TimeSlot
                            {
                                Date = startDate.Date,
                                StartTime = currentTime.TimeOfDay,
                                EndTime = currentTime.Add(slotDuration).TimeOfDay,
                                IsTaken = false 
                            };
                            timeSlots.Add(timeSlot);
                            await _context.TimeSlots.AddAsync(timeSlot);
                            await _context.SaveChangesAsync();
                            currentTime = currentTime.Add(slotDuration);
                    }
                }
            }

            startDate = startDate.AddDays(1); // Move to the next day
        }
        
        return timeSlots;
    }
}