using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptiApp.Models;
using OptiApp.Service;
using OptiApp.ViewModel;

namespace OptiApp.Controllers;

[Authorize]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserStore<IdentityUser> _userStore;

    public AdminController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IUserStore<IdentityUser> userStore)
    {
        _context = context;
        _userManager = userManager;
        _userStore = userStore;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var resultView =await AdminService.CreateResultAsync(_context);
        return View(resultView);
    }

    public IActionResult CreateOptometristAccount()
    {
        return View(new OptometristRequestViewModel());
    }

    [HttpPost]
    public  async Task<IActionResult> CreateOptometristAccount(OptometristRequestViewModel optometrist)
    {
        var identityUser = CreateUser();
        var password = GeneratePassword(optometrist.Name, optometrist.Surname, optometrist.DoB.ToString("dd/MM/yyyy"));
        await _userStore.SetUserNameAsync(identityUser, optometrist.Email, CancellationToken.None);
        identityUser.Email = optometrist.Email;
        var newUser = await _userManager.CreateAsync(identityUser, password);
        await _userManager.AddToRoleAsync(identityUser, Roles.Optometrist.ToString());
        if (newUser.Succeeded)
        {
            optometrist.IsLinked = true;
            var opt = new Optometrist()
            {
                Name = optometrist.Name,
                Surname = optometrist.Surname,
                IsLinked = optometrist.IsLinked,
                Cellphone = optometrist.Cellphone,
                Email = optometrist.Email,
                DoB = optometrist.DoB.ToString("MM/dd/yyyy"),
                Address = optometrist.Address
            };
            await _context.Optometrists.AddAsync(opt);
            await _context.SaveChangesAsync();
            ViewBag.Success = "Linked Optometrist Successful";
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> LinkOptometristToWeb(int id)
    {
        var opt = await _context.Optometrists.FirstOrDefaultAsync(op => op.OptometristId.Equals(id));
        var user = await _userManager.FindByEmailAsync(opt!.Email);
        if (user is null)
        {
            var identityUser = CreateUser();
            var password = GeneratePassword(opt.Name, opt.Surname, opt.DoB);
            await _userStore.SetUserNameAsync(identityUser, opt.Email, CancellationToken.None);
            identityUser.Email = opt.Email;
            var newUser = await _userManager.CreateAsync(identityUser, password);
            await _userManager.AddToRoleAsync(identityUser, Roles.Optometrist.ToString());
            if (newUser.Succeeded)
            {
                opt.IsLinked = true;
                _context.Optometrists.Update(opt);
                await _context.SaveChangesAsync();
                ViewBag.Success = "Linked Optometrist Successful";
                return RedirectToAction("Index");
            }
        }
        
        return RedirectToAction("Index");
    }
    
    private IdentityUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<IdentityUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. " +
                                                $"Ensure that '{nameof(User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                                                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        }
    }

    private string GeneratePassword(string firstname, string lastname, string dob)
    {
        var data = dob.Split('/');
        var password = string.Empty;
        foreach (var str in data)
        {
            password += str;
        }
        var pass = firstname.Substring(0, 1) + lastname.Substring(0, 1).ToLower() + "@" +password ;
        return pass;
    }
}