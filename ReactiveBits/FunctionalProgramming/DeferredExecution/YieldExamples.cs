using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ReactiveBits.FunctionalProgramming.DeferredExecution
{
    public class YieldExamples
    {
        public class SomeClass
        {
            public static IEnumerable<string> Coroutine()
            {
                yield return "foo";
                yield return "bar";
                yield return "baz";
            }
        }

        [Fact]
        public void coroutines_can_be_queried_with_LINQ()
        {
            var values = from v in SomeClass.Coroutine() select v;

            values.Should().Contain("foo");
            values.Should().Contain("bar");
            values.Should().Contain("baz");
        }
    }
}