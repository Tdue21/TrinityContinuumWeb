using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using TrinityContinuum.Models;
using TrinityContinuum.WebApp.Components.SheetSections;
using TrinityContinuum.WebApp.Services;
using Xunit;

namespace TrinityContinuum.UITests.SheetSections
{
    //public class InjuryConditionsBaseTests : SheetSectionBase
    //{
    //    [Theory]
    //    [InlineData(1, false, 3)]
    //    [InlineData(2, true, 4)]
    //    [InlineData(3, false, 4)]
    //    [InlineData(5, false, 5)]
    //    [InlineData(5, true, 6)]
    //    public void Test_Stamina_Effect_On_Injury_Conditions(int stamina, bool hasEndurance, int expectedLines)
    //    {
    //        var toast = Substitute.For<ToastService>();
    //        var charService = Substitute.For<ICharacterService>();

    //        Services.AddSingleton(toast);
    //        Services.AddSingleton(charService);

    //        // Arrange
    //        var cut = RenderComponent<InjuryConditions>(parameters =>
    //        {
    //            parameters.Add(p => p.Model, CreateCharacter(stamina, hasEndurance));
    //        });

    //        // Act
    //        var lines = cut.FindAll("input[type='checkbox']");

    //        // Assert
    //        cut.Markup.Should().NotBeNullOrEmpty();
    //        lines.Should().HaveCount(expectedLines);
    //    }

    //    private Character CreateCharacter(int stamina, bool hasEndurance)
    //    {
    //        var character = CreateCharacter();
    //        character.Attributes.Physical["Stamina"] = stamina;

    //        if (hasEndurance)
    //        {
    //            character.Edges.Add(new Edge
    //            {
    //                Name = "Endurance",
    //                Dots = 1,
    //            });
    //        }

    //        return character;
    //    }
    //}
}
