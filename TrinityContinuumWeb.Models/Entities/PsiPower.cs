namespace TrinityContinuum.Models.Entities;

public class PsiPower
{
    public required string Aptitude { get; set; }
    public required string Mode { get; set; }
    public required string Name { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? System { get; set; }
    public int Dots { get; set; }
    public string? Cost { get; set; }
    public string? Source { get; set; }
    public int Page { get; set; }
}
