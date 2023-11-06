// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OptiApp.Models;
using OptiApp.ViewModel;

namespace OptiApp.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        [BindProperty]
        public  ProfileViewModel ProfileViewModel { get; set; }
        

        public async Task<IActionResult> OnGetAsync()
        {
            
            var patient =
                await _context.Patients.SingleOrDefaultAsync(u => u.Email.Equals(HttpContext.User.Identity!.Name));
            var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity!.Name);
            var userRole = await _userManager.GetRolesAsync(user);
            if (patient is null)
            {
                if (userRole[0].Equals(Roles.Admin.ToString()))
                {
                    return RedirectToAction("Index","Admin");
                }

                if (userRole[0].Equals(Roles.Optometrist.ToString()))
                {
                    return RedirectToAction("Index","Opt");
                }
            }
            var query = from booking in _context.Bookings
                join timeslot in _context.TimeSlots on booking.TimeSlotId equals timeslot.Id
                where booking.PatientId == patient.PatientId
                select new BookingHistoryViewModel
                {
                    BookingId = booking.BookingId,
                    Date = booking.Date,
                    TimeSlot = timeslot.StartTime + "-" + timeslot.EndTime,
                    Services = booking.Services,
                    TotalAmount = booking.TotalAmount,
                };
            var bookingHistory = await query.ToListAsync();
            var profile = new ProfileViewModel()
            {
                User = patient,
                BookingHistory = bookingHistory
                
            };
            ProfileViewModel = profile;
            return Page();
        }
    }
}
