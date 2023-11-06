using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OptiApp.Models;
using OptiApp.ViewModel;

namespace OptiApp.Service;


public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context;

    public UserService(UserManager<IdentityUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<CurrentUserVm> GetLoggedInUserFullName(HttpContext context)
    {
        if (!context.User.Identity!.IsAuthenticated)
        {
            return new CurrentUserVm()
            {
                FullName = "Guest"
            };
        }
        else
        {
            var user = await _userManager.FindByEmailAsync(context.User.Identity!.Name);
            var userRoles = await _userManager.GetRolesAsync(user);
            var userRole = userRoles.First();

            if (userRole.Equals(Roles.Admin.ToString()))
            {
                return new CurrentUserVm()
                {
                    FullName = "ADMIN",
                    Role = Roles.Admin
                };
            }

            if (userRole.Equals(Roles.Optometrist.ToString()))
            {
                var userInfo = await _context.Optometrists.FirstOrDefaultAsync(o => o.Email.Equals(user.Email));
                return new CurrentUserVm()
                {
                    FullName = userInfo!.Name + " " + userInfo.Surname,
                    Role = Roles.Optometrist
                };
            }

            if (userRole.Equals(Roles.User.ToString()))
            {
                var userInfo = await _context.Patients.FirstOrDefaultAsync(o => o.Email!.Equals(user.Email));
                return new CurrentUserVm()
                {
                    FullName = userInfo!.Name + " " + userInfo.Surname,
                    Role = Roles.User
                };
            }
        }
        return new CurrentUserVm();
    }
}