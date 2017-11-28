using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;

namespace ReactiveBits.CreatingObservables.Handcrafted
{
    public class NumberObservableTest
    {
        [Fact]
        public void should_return_all_the_items()
        {
            var observable = new NumberObservable(10);

            var sb = new List<string>();
            using (observable.SubscribeString(sb))
            {
            }

            sb[0].Should().Be("Received 0");
            sb[1].Should().Be("Received 1");
            sb[2].Should().Be("Received 2");
            sb[3].Should().Be("Received 3");
            sb[4].Should().Be("Received 4");
            sb[5].Should().Be("Received 5");
            sb[6].Should().Be("Received 6");
            sb[7].Should().Be("Received 7");
            sb[8].Should().Be("Received 8");
            sb[9].Should().Be("Received 9");
            sb[10].Should().Be("Done");
        }
    }
}