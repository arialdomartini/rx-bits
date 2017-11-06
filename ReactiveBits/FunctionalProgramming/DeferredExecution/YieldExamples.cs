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

            public IEnumerable<string> AnotherCoroutine()
            {
                Step = 1;
                yield return "foo";

                Step = 2;
                yield return "bar";

                Step = 3;
                yield return "baz";
            }

            public int Step { get; set; }
        }

        [Fact]
        public void coroutines_can_be_queried_with_LINQ()
        {
            var values = from v in SomeClass.Coroutine() select v;

            values.Should().Contain("foo");
            values.Should().Contain("bar");
            values.Should().Contain("baz");
        }

        [Fact]
        public void coroutines_can_be_iterated()
        {
            var sut = new SomeClass();
            sut.Step.Should().Be(0);

            var iterator = sut.AnotherCoroutine().GetEnumerator();

            iterator.MoveNext().Should().Be(true);
            iterator.Current.Should().Be("foo");
            sut.Step.Should().Be(1);
            
            iterator.MoveNext().Should().Be(true);
            iterator.Current.Should().Be("bar");
            sut.Step.Should().Be(2);
            
            iterator.MoveNext().Should().Be(true);
            iterator.Current.Should().Be("baz");
            sut.Step.Should().Be(3);

            iterator.MoveNext().Should().Be(false);
        }
    }
}