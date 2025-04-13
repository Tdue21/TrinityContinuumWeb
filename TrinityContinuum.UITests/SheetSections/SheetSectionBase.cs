using System.Collections.Generic;
using TrinityContinuumWeb.Models;

namespace TrinityContinuum.UITests.SheetSections
{
    public abstract class SheetSectionBase : TestContext
    {

        protected Character CreateCharacter()
        {
            var character = new Character
            {
                Name = "",
                OriginPath = new(),
                RolePath = new(),
                SocietyPath = new(),
                Attributes =
                {
                    Physical = new Dictionary<string, int>
                    {
                        { "Might", 1 },
                        { "Dexterity", 1 },
                        { "Stamina", 1 }
                    },
                    Mental = new Dictionary<string, int>
                    {
                        { "Intellect", 1 },
                        { "Cunning", 1 },
                        { "Resolve", 1 },
                    },
                    Social = new Dictionary<string, int>
                    {
                        { "Presence", 1 },
                        { "Manipulation", 1 },
                        { "Composure", 1 }
                    }
                },
                Skills =
                {
                    { "Aim",  new() { Dots = 0 } },
                    { "Athletics",  new() { Dots = 0 } },
                    { "Close Combat", new() { Dots = 0 } },
                    { "Command",   new() { Dots = 0 } },
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
                    { "Survival", new() { Dots = 0 } },
                    { "Technology", new() { Dots = 0 } },
                }
            };

            return character;
        }
    }
}
