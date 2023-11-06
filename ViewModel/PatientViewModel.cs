namespace OptiApp.ViewModel;

public class PatientViewModel
{
    public int PatientId { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Cellphone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string DoB { get; set; } = null!;
    public string? Email { get; set; }
}