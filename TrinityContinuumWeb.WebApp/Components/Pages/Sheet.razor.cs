using Microsoft.AspNetCore.Components;
using TrinityContinuumWeb.Models;

namespace TrinityContinuum.WebApp.Components.Pages;

public class SheetComponent : ComponentBase
{
    public Character Model { get; set; } = new Character
    {
        Name = "John Doe",
        Player = "Jane Doe",
        Concept = "Concept",
        PsiOrder = "Psi Order",
        OriginPath = new() { Name = "Origin Path", Dots = 1 },
        RolePath = new() { Name = "Role Path", Dots = 1 },
        SocietyPath = new() { Name = "Society Path", Dots = 1 },
        Skills =
        [
            new() { Name = "Aim", Dots = 0, Specialties = ["Pistols", "SMGs"], Tricks = ["Trick Shot"] },
            new() { Name = "Athletics", Dots = 0 },
            new() { Name = "Close Combat", Dots = 0 },
            new() { Name = "Command", Dots = 0 },
            new() { Name = "Culture", Dots = 0 },
            new() { Name = "Empathy", Dots = 0 },
            new() { Name = "Enigmas", Dots = 0 },
            new() { Name = "Humanities", Dots = 0 },
            new() { Name = "Integrity", Dots = 0 },
            new() { Name = "Larceny", Dots = 0 },
            new() { Name = "Medicine", Dots = 0 },
            new() { Name = "Persuasion", Dots = 0 },
            new() { Name = "Pilot", Dots = 0 },
            new() { Name = "Science", Dots = 0 },
            new() { Name = "Survival", Dots = 0 },
            new() { Name = "Technology", Dots = 0 },
        ],
        Attributes =
        {
            PreferredApproach = PreferredApproach.Finesse,
            Mental =
            {
                new() { Name = "Intellect", Dots = 2 },
                new() { Name = "Cunning", Dots = 3 },
                new() { Name = "Resolve", Dots = 3 },
            },
            Physical =
            {
                new() { Name = "Might", Dots = 3 },
                new() { Name = "Dexterity", Dots = 2 },
                new() { Name = "Stamina", Dots = 5 },
            },
            Social =
            {
                new() { Name = "Presence", Dots = 3 },
                new() { Name = "Manipulation", Dots = 2 },
                new() { Name = "Composure", Dots = 3 },
            },
        },
        Edges =
        {
            new() { Name = "Endurance", Dots = 1 },
        },
    };
}
/*
public class Character
{
    public required string Name { get; set; }
    public string? Player { get; set; }
    public string? Concept { get; set; }
    public string? PsiOrder { get; set; }

    public required Trait OriginPath { get; set; }
    public required Trait RolePath { get; set; }
    public required Trait SocietyPath { get; set; }

    public IEnumerable<Skill>? Skills { get; set; } 
}

public class  Trait 
{
    public required string Name { get; set; }
    public int Value { get; set; }
}

public class Skill : Trait
{
    public string[]? Specialties { get; set; }
    public string[]? Tricks { get; set; }
}
*/