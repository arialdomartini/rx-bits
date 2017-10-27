using System;
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

        [Fact]
        public void closures_maintain_value_of_variables_defined_outside_their_scope()
        {
            Func<string> func = null;

            {
                var variable = "first value";

                func = delegate()
                {
                    return variable;
                };
            }

            func().Should().Be("first value");
        }

        [Fact]
        public void closures_share_the_same_reference_to_captured_variables()
        {
            Func<string> func1 = null;
            Func<string> func2 = null;

            {
                var variable = "first value";

                func1 = delegate()
                {
                    variable = "second value";
                    return variable;
                };

                func2 = delegate()
                {
                    return variable;
                };
            }

            func2().Should().Be("first value");
            func1().Should().Be("second value");
            func1().Should().Be("second value");
        }

        [Fact]
        public void lambda_expressions_are_an_alternative_syntax_for_anonymous_delegates()
        {
            var areSimilar = new AreSimilar();

            var actual = areSimilar.Check(
                new List<string> {"one", "two", "three"},
                new List<string> {"uno", "due", "e tre"},
                (s1, s2) => s1.Length == s2.Length );

            actual.Should().BeTrue();
        }

        [Fact]
        public void lambda_expressions_can_be_closures_too()
        {
            Func<string> func = null;

            {
                const string variable = "first value";

                func = () => variable;
            }

            func().Should().Be("first value");
        }
    }
}