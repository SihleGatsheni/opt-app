namespace OptiApp.ViewModel;

public class OResultViewModel
{
    public IEnumerable<OptometristResultViewModel>? Data { get; set; }
    public int RegisteredClientCount { get; set; }
    public int RegisteredOptometristCount { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}