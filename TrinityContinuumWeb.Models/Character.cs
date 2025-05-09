namespace TrinityContinuum.Models;

public class CharacterSummary
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Player { get; set; }
}

public class Character
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Player { get; set; }
    public string? Concept { get; set; }
    public string? Description { get; set; }
    public string? Background { get; set; }
    public string? Token { get; set; }
    public List<Aspiration> Aspirations { get; } = new();
    public string? PsiOrder { get; set; }
    public required PathTrait OriginPath { get; set; }
    public required PathTrait RolePath { get; set; }
    public required PathTrait SocietyPath { get; set; }
    public Dictionary<string, Skill> Skills { get; set; } = new();
    public Attributes Attributes { get; } = new();
    public Psi Psi { get;  } = new Psi();
    public List<Edge> Edges { get; } = new();
    public int Defense { get; set; }
    public Armor Armor { get; } = new();
    public Experience Experience { get; set; } = new();
    public bool Statblock { get; set; }
    public string? Source { get; set; }
}

public class Experience
{
    public int Total { get; set; }
    public int Spent { get; set; }
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


//public class Skills
//{
//    public Skill Aim { get; set; } = new();
//	public Skill Athletics { get; set; } = new();
//	public Skill CloseCombat { get; set; } = new();
//	public Skill Command { get; set; } = new();
//	public Skill Culture { get; set; } = new();
//	public Skill Empathy { get; set; } = new();
//	public Skill Enigmas { get; set; } = new();
//	public Skill Humanities { get; set; } = new();
//	public Skill Integrity { get; set; } = new();
//	public Skill Larceny { get; set; } = new();
//	public Skill Medicine { get; set; } = new();
//	public Skill Persuasion { get; set; } = new();
//	public Skill Pilot { get; set; } = new();
//	public Skill Science { get; set; } = new();
//	public Skill Survival { get; set; } = new();
//    public Skill Technology { get; set; } = new();
//}

public enum PreferredApproach
{
    Force,
    Finesse,
    Resilience
}

public class Attributes
{
    public PreferredApproach PreferredApproach { get; set; } = PreferredApproach.Force;
    
    public Dictionary<string, int> Mental { get; set; } = new();
    public Dictionary<string, int> Physical { get; set; } = new();
    public Dictionary<string, int> Social { get; set; } = new();
}

public class Psi
{
    public string[] Aptitudes { get; set; } = [];
    public int Trait { get; set; }
    public int Tolerance { get; set; }
    public List<string> BasicPowers { get; } = [];
    public List<Trait> Modes { get; } = [];
    public List<Trait> AuxillaryModes { get;  } = [];
}
