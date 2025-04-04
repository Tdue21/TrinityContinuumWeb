using System.Text.Json.Serialization;

namespace TrinityContinuumWeb.Models;
public class Character
{
    public required string Name { get; set; }
    public string? Player { get; set; }
    public string? Concept { get; set; }
    public string? Description { get; set; }
    public string? Background { get; set; }
    public List<Aspiration> Aspirations { get; } = new List<Aspiration>();
    public string? PsiOrder { get; set; }
    public required PathTrait OriginPath { get; set; }
    public required PathTrait RolePath { get; set; }
    public required PathTrait SocietyPath { get; set; }
    public List<Skill> Skills { get; set; } = new List<Skill>();
    public Attributes Attributes { get; } = new Attributes();
    public Psi Psi { get;  } = new Psi();
    public List<Edge> Edges { get; } = new List<Edge>();
    public int Defense { get; set; }
    public Armor Armor { get; } = new Armor();
    public int Experience { get; set; }
    public bool Statblock { get; set; }
    public string? Source { get; set; }
}

public class Armor
{
    public int Soft { get; set; }
    public int Hard { get; set; }
}

public class Aspiration
{
    public bool IsLongTerm { get; set; }
    public required string Description { get; set; }
}

public class Trait
{
    public string? Name { get; set; }
    public int Dots { get; set; }
}

public class Edge : Trait
{
    public string? Note { get; set; }
}

public class PathTrait : Trait
{
    public List<Contact> Contacts { get; } = new List<Contact>();
}

public class Contact : Trait
{
    public string[]? Tags { get; set; }
}

public class Skill : Trait
{
    public List<string> Specialties { get; set; } = new List<string>();
    public List<string> Tricks { get; set; } = new List<string>();
}

public enum PreferredApproach
{
    Force,
    Finesse,
    Resilience
}

public class Attributes
{
    [JsonPropertyName("preferredApproach")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PreferredApproach PreferredApproach { get; set; } = PreferredApproach.Force;

    public List<Trait> Mental { get; set; } = new List<Trait>();
    public List<Trait> Physical { get; set; } = new List<Trait>();
    public List<Trait> Social { get; set; } = new List<Trait>();
}

public class Psi
{
    public string Aptitude { get; set; } = string.Empty;
    public int Trait { get; set; }
    public int Tolerance { get; set; }
    public List<string> BasicPowers { get; } = new List<string>();
    public List<Trait> Modes { get;  } = new List<Trait>();
}
