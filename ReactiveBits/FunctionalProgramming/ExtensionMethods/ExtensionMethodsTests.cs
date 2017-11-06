using FluentAssertions;
using Xunit;

namespace ReactiveBits.FunctionalProgramming.ExtensionMethods
{
    public class Person
    {
        internal string Name;

        public Person(string name)
        {
            Name = name;
        }
    }

    public static class SomeExtensionMethods
    {
        public static string Greet(this Person person, string day)
        {
            return $"Hallo, {person.Name}, today it's {day}!";
        }
    }

    public class ExtensionMethodsTests
    {
        [Fact]
        public void extension_method_invocation_is_just_syntactic_sugar()
        {
            var person = new Person("Mario");

            var withExtensionMethod = person.Greet("Monday");
            var withoutExtensionMethod = SomeExtensionMethods.Greet(person, "Monday");
            
            withoutExtensionMethod.Should().Be(withExtensionMethod);
        }

    }
}