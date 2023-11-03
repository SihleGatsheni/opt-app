using Microsoft.EntityFrameworkCore;
using OptiApp.Models;
using OptiApp.ViewModel;

namespace OptiApp.Service;

public class AdminService
{
    private readonly ApplicationDbContext _context;
    private static OResultViewModel? Result { get; set; }

    private AdminService(ApplicationDbContext context)
    {
        _context = context;
    }
    private async Task<IEnumerable<OptometristResultViewModel>> GetRegisteredOptometrists()
    {
        return await _context.Optometrists.Select(opt => new OptometristResultViewModel()
        {
            OptometristId = opt.OptometristId,
            FullName = opt.Name + " " + opt.Surname,
            Email = opt.Email,
            DoB = opt.DoB,
            Address = opt.Address,
            Cellphone = opt.Cellphone,
            IsLinked = opt.IsLinked
        }).ToListAsync();
    }

    private async Task<int> RegisteredClientCount()
    {
        return  await _context.Patients.CountAsync();
    }
    private async Task<int> RegisteredOptometristCount()
    {
        return await _context.Optometrists.CountAsync();
    }


    public static async Task<OResultViewModel> CreateResultAsync(ApplicationDbContext context)
    {
        Result = new OResultViewModel();
        AdminService service = new(context);
        Result.Data =  await service.GetRegisteredOptometrists();
        Result.RegisteredClientCount = await service.RegisteredClientCount();
        Result.RegisteredOptometristCount = await service.RegisteredOptometristCount();
        Result.TotalPages = await context.Optometrists.CountAsync();
        Result.CurrentPage = await context.Optometrists.CountAsync();
        return Result;
    }
}