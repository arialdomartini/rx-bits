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
    }
}