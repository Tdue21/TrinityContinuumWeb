using FluentAssertions;
using Newtonsoft.Json;
using TrinityContinuumWeb.Models;

namespace TrinityContinuumWeb.Tests;

public class CharacterSerializationTests
{
    [Fact]
    public void ObjectToJson()
    {
        var c2 = JsonConvert.DeserializeObject<Character>(File.ReadAllText("character.json"));
        var character1 = JsonConvert.SerializeObject(c2, Formatting.Indented);
        var character2 = JsonConvert.SerializeObject(_character, Formatting.Indented);

        character1.Should().BeEquivalentTo(character2);
    }

    [Fact]
    public void JsonToObject()
    {
        var character1 = JsonConvert.DeserializeObject<Character>(File.ReadAllText("character.json"));
        var character2 = _character;

        character1.Should().BeEquivalentTo(character2);
    }

    private Character _character = new Character
    {
        Name = "Test Character",
        Player = "Test Player",
        Concept = "Test Concept",
        Description = "Test Description",
        Background = "Test Background",
        Aspirations =
        {
            new() { IsLongTerm = true, Description = "Test Long Term Aspiration" },
            new() { IsLongTerm = false, Description = "Test Short Term Aspiration" },
            new() { IsLongTerm = false, Description = "Test Short Term Aspiration" }
        },
        OriginPath = new() { Name = "Test Origin", Dots = 1 },
        RolePath = new() { Name = "Test Role", Dots = 1 },
        SocietyPath = new() { Name = "Test Society", Dots = 1 },
        Attributes =
        {
            PreferredApproach = PreferredApproach.Force,
            Physical =
            {
                new() { Name = "Might", Dots = 1 },
                new() { Name = "Dexterity", Dots = 1 },
                new() { Name = "Stamina", Dots = 1 },
            },
            Social =
            {
                new() { Name = "Presence", Dots = 1 },
                new() { Name = "Manipulation", Dots = 1 },
                new() { Name = "Composure", Dots = 1 },
            },
            Mental =
            {
                new() { Name = "Intellect", Dots = 1 },
                new() { Name = "Cunning", Dots = 1 },
                new() { Name = "Resolve", Dots = 1 },
            }
        },
        Skills =
        {
            new() { Name = "Aim", Dots = 0 },
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
        },
        Armor = { Soft = 0, Hard = 0 },
        Defense = 0,
        Experience = 0,
        Statblock = false,
        Source = "Test Source",
        Edges =
        {
            new() { Name = "Test Edge", Dots = 1 }
        },
        Psi =
        {
            Aptitude = "Test Aptitude",
            Trait = 1,
            Tolerance = 0,
            BasicPowers = { "Basic Power 1", "Basic Power 2" },
            Modes =
            {
                new() { Name = "Test Mode", Dots = 1 },
                new() { Name = "Test Mode", Dots = 1 },
                new() { Name = "Test Mode", Dots = 1 }
            }
        }
    };
}