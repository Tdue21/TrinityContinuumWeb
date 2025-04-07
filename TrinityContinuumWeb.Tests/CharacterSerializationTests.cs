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
            PreferredApproach = PreferredApproach.Finesse,
            Physical =
            {
                { "Might", 1 },
                { "Dexterity", 1 },
                { "Stamina", 1 },
            },
            Social =
            {
                { "Presence", 1 },
                { "Manipulation", 1 },
                { "Composure", 1 },
            },
            Mental =
            {
                { "Intellect", 1 },
                { "Cunning",  1 },
                { "Resolve",  1 },
            }
        },
        Skills =
        {
            { "Aim", new() { Dots = 0 } },
            { "Athletics", new() { Dots = 0 } },
            { "Close Combat", new() { Dots = 0 } },
            { "Command", new() { Dots = 0 } },
            { "Culture", new() { Dots = 0 } },
            { "Empathy", new() { Dots = 0 } },
            { "Enigmas", new() { Dots = 0 } },
            { "Humanities", new() { Dots = 0 } },
            { "Integrity", new() { Dots = 0 } },
            { "Larceny", new() { Dots = 0 } },
            { "Medicine", new() { Dots = 0 } },
            { "Persuasion", new() { Dots = 0 } },
            { "Pilot", new() { Dots = 0 } },
            { "Science", new() { Dots = 0 } },
            { "Survival", new() { Dots = 0 }  },
            { "Technology", new() { Dots = 0 } },
        },
        Armor = { Soft = 0, Hard = 0 },
        Defense = 0,
        Experience = { Total = 0, Spent = 0},
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