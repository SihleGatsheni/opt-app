namespace OptiApp.ViewModel;

public class OptometristResultViewModel
{
    public int OptometristId { get; set; }
    public string FullName { get; set; } = null!;
    public string DoB { get; set; } = null!;
    public string Cellphone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
    public bool IsLinked { get; set; }
}