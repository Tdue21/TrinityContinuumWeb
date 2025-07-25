namespace TrinityContinuum.Models.Entities;

public class Weapon
{
    public string? FileName { get; set; }
    public string? Model { get; set; }
    public string? Category { get; set; }
    public string? Type { get; set; }
    public string? Enhancement { get; set; }
    public string? Range { get; set; }
    public string? DamageType { get; set; }
    public IEnumerable<Tag> Wtags { get; set; } = Enumerable.Empty<Tag>();
    public IEnumerable<string> ammo { get; set; } = Enumerable.Empty <string> ();
    public string? Tech { get; set; }
    public string? FT { get; set; }
    public string? Size { get; set; }
    public string? Cost { get; set; }
    public string? Source { get; set; }
    public int Page { get; set; }
}

public class Tag
{
    public string? Name { get; set; }
    public int Value { get; set; }
}
