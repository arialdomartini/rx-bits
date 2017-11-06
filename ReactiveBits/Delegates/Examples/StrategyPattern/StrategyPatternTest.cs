using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ReactiveBits.Delegates.Examples.StrategyPattern
{
    public class StrategyPatternTest
    {
        public class LenghtComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                if (x.Length == y.Length) return 0;
                return x.Length < y.Length ? 1 : 2;
            }
        }

        [Fact]
        public void classic_strategy_pattern()
        {
            var words = new[] {"short", "very very very long", "long enough"};
            
            words.ToList().Sort(new LenghtComparer());

            words.Should().BeEquivalentTo("short", "long enough", "very very very long");
        }


        class GenericComparer<T> : IComparer<T>
        {
            private readonly Func<T, T, int> _comparer;

            public GenericComparer(Func<T, T, int> comparer)
            {
                _comparer = comparer;
            }

            public int Compare(T x, T y)
            {
                return _comparer(x, y);
            }
        }

        [Fact]
        public void strategy_pattern_implemented_with_lambdas()
        {
            var words = new[] {"short", "very very very long", "long enough"};

            words.ToList().Sort(
                new GenericComparer<string>( (x, y) => 
                    {
                        if (x.Length == y.Length) return 0;
                        return x.Length < y.Length ? 1 : 2;
                    }));

            words.Should().BeEquivalentTo("short", "long enough", "very very very long");
        }
    }
}

namespace ReactiveBits.FunctionalProgramming.Examples.LazyLoading
{
}