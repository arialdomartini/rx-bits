using System;
using System.Runtime.ExceptionServices;
using FluentAssertions;
using Xunit;

namespace ReactiveBits.Delegates
{
    public class Delegates
    {
        // A delegate is a type that represents references to methods
        public delegate bool ComparisonDelegate(string a, string b);
    }

    public class StringComparators
    {
        public static bool CompareContent(string a, string b)
        {
            return a == b;
        }

        public static bool CompareLength(string a, string b)
        {
            return a.Length == b.Length;
        }
    }

    public class DelegatesTest
    {
        [Fact]
        public void delegates_can_be_used_as_types()
        {
            var compare = new Delegates.ComparisonDelegate(StringComparators.CompareLength);
            compare("gio", "leo").Should().Be(true);

            compare = new Delegates.ComparisonDelegate(StringComparators.CompareContent);
            compare("gio", "leo").Should().Be(false);
        }
    }
}