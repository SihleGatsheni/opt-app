using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OptiApp.Models;
using OptiApp.ViewModel;

namespace OptiApp.Areas.Identity.Pages.Account.Manage;

public class Edit : PageModel
{
    private readonly ApplicationDbContext _context;

    public Edit(ApplicationDbContext context)
    {
        _context = context;
    }
    [BindProperty]
    public  Patient ProfileViewModel { get; set; }
    public  async Task OnGetAsync()
    {
       var profile = await _context.Patients.SingleOrDefaultAsync(u => u.Email.Equals(HttpContext.User.Identity!.Name));
       ProfileViewModel = profile!;
    }

    public async Task<IActionResult> OnPostAsync(Patient patient)
    {
        var profile = await _context.Patients.SingleOrDefaultAsync(u => u.Email.Equals(HttpContext.User.Identity!.Name));
        if (profile is not null)
        {
            profile.Name = patient.Name;
            profile.Cellphone = patient.Cellphone;
            profile.Address = patient.Address;
            _context.Update(profile);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        return Page();
    }
}