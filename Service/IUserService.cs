using OptiApp.ViewModel;

namespace OptiApp.Service;

public interface IUserService
{
    Task<CurrentUserVm> GetLoggedInUserFullName(HttpContext context);
}