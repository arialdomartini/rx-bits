using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using static ReactiveBits.Delegates.Delegates;

namespace ReactiveBits.Delegates
{
    public class DelegatesTest
    {
        [Fact]
        public void delegates_can_be_used_as_types()
        {
            var compare1 = new ComparisonDelegate(StringComparators.CompareLength);
            compare1("gio", "leo").Should().Be(true);

            var compare2 = new ComparisonDelegate(StringComparators.CompareContent);
            compare2("gio", "leo").Should().Be(false);
        }

        [Fact]
        public void delegates_can_be_passed_as_first_order_functions()
        {
            var areSimilar = new AreSimilar();

            var actual = areSimilar.Check(
                new List<string> {"one", "two", "three"},
                new List<string> {"uno", "due", "e tre"},
                new ComparisonDelegate(StringComparators.CompareLength));

            actual.Should().BeTrue();
        }

        [Fact]
        public void anonymous_delegates_can_be_defined_inline()
        {
            var areSimilar = new AreSimilar();

            var actual = areSimilar.Check(
                new List<string> {"one", "two", "three"},
                new List<string> {"uno", "due", "e tre"},
                delegate(string s1, string s2) { return s1.Length == s2.Length; });

            actual.Should().BeTrue();
        }
    }
}