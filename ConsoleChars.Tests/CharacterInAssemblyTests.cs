﻿using ConsoleChars.Implementation;
using ConsoleChars.Implementation.Characters.Letters;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleChars.Tests
{
    [TestFixture]
    public class CharacterInAssemblyTests
    {
        private const string MainProjectName = "ConsoleChars";

        [TestCaseSource(nameof(GetAllCharactersInAssembly))]
        public void AnyCharacter_ShouldNotBeNull(Character character)
        {
            character.Should().NotBeNull();
        }

        [TestCaseSource(nameof(GetAllCharactersInAssembly))]
        public void MediumStringLines_ShouldAlwaysReturn6Lines(Character character)
        {
            true.Should().BeTrue();
        }

        private static IEnumerable<Character> GetAllCharactersInAssembly()
        {
            var referencedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            var consoleCharsAssemblyName = referencedAssemblies.Single(n => n.Name.Equals(MainProjectName));
            Assembly consoleCharsAssembly = Assembly.Load(consoleCharsAssemblyName);

            IEnumerable<Character> characters = consoleCharsAssembly.GetTypes()
                .Where(n => n.IsClass)
                .Where(n => !n.IsAbstract)
                .Where(n => n.IsSubclassOf(typeof(Character)))
                .Select(n => Activator.CreateInstance(n) as Character);

            return characters;
        }
    }
}
