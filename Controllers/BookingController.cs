using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptiApp.Models;
using OptiApp.Service;
using OptiApp.ViewModel;

namespace OptiApp.Controllers;


[Authorize]
public class BookingController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly BookingService _bookingService;
    private readonly SlotGeneratorService _service;
    private readonly EmailSender _emailSender;

    public BookingController(ApplicationDbContext context,IConfiguration configuration)
    {
        _context = context;
        _bookingService = new BookingService(context);
        _service = new SlotGeneratorService(context);
        _emailSender = new EmailSender(configuration);
    }
    public async Task<IActionResult> Booking()
    {
        var user = await _context.Patients.SingleOrDefaultAsync(user => user.Email!.Equals(HttpContext.User.Identity!.Name));
        var model = new AppointmentViewModel()
        {
        PatientId = user!.PatientId.ToString(),
        Address = user!.Address,
        Email = user.Email,
        };
        return View(model);
    }

    [HttpGet("/api/slots/{date}")]
    public async Task<IActionResult> Slots([FromRoute]DateTime date)
    {
        var slots = await _service.GenerateTimeSlots(DateTime.Today, DateTime.Today.AddDays(7));
        var selectedDateSlots = slots.Where(slot => slot.Date == date && (date != DateTime.Today || (slot.StartTime > DateTime.Now.TimeOfDay && slot.EndTime > DateTime.Now.TimeOfDay)))
            .ToList();
        return Ok(selectedDateSlots);
    }
    
    [HttpPost("api/booking")]
    public async Task<IActionResult> BookAppointment([FromBody]BookingViewModel booking)
    {
        var booked = await _bookingService.Book(booking);
        return Ok(booked);
    }
    
    private string GenerateUniqueAppointmentId(string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            throw new ArgumentException("User name cannot be empty.");
        }
        
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff"); // Timestamp
        string randomPart = Guid.NewGuid().ToString("N").Substring(0, 4); // Random part (4 characters)
        
        string firstCharacter = userName.Substring(0, 1);

        // Combine the user's name initial, hyphen, timestamp, and random part to create the unique ID
        string uniqueId = $"{firstCharacter}-{timestamp}{randomPart}";

        return uniqueId;
    }
}