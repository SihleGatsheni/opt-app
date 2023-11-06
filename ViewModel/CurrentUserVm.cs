using OptiApp.Models;

namespace OptiApp.ViewModel;

public class CurrentUserVm
{
    public string? FullName { get; set; } = "Guest";
    public Roles Role { get; set; }
}