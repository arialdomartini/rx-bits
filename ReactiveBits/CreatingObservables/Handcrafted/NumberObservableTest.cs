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

            sb[0].Should().Be("OnNext(0)");
            sb[1].Should().Be("OnNext(1)");
            sb[2].Should().Be("OnNext(2)");
            sb[3].Should().Be("OnNext(3)");
            sb[4].Should().Be("OnNext(4)");
            sb[5].Should().Be("OnNext(5)");
            sb[6].Should().Be("OnNext(6)");
            sb[7].Should().Be("OnNext(7)");
            sb[8].Should().Be("OnNext(8)");
            sb[9].Should().Be("OnNext(9)");
            sb[10].Should().Be("OnCompleted()");
        }
    }
}