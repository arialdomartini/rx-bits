using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ReactiveBits.FunctionalProgramming.DeferredExecution
{
    public class DeferredExecutionExamples
    {
        [Fact]
        public void LINQ_queries_are_executed_on_demand()
        {
            var ints = new List<int> {1, 2, 3, 4, 5};

            var even = from i in ints
                where i % 2 == 0
                select i;

            ints.Add(6);

            even.Should().Contain(6);
        }
    }
}