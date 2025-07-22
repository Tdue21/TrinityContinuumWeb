namespace TrinityContinuum.Models.Dtos;

public class CharacterSummary
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Player { get; set; }
}
