namespace OptiApp.ViewModel;

public class OptometristRequestViewModel
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Cellphone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateTime DoB { get; set; }
    public string Email { get; set; } = null!;
    public string? UserId { get; set; }
    public bool IsLinked { get; set; }
}