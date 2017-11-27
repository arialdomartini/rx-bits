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

            var result = new StringBuilder();

            using (observable.SubscribeString(result))
            {
            }

            var actual = Regex.Split(result.ToString(), "\r\n");


            actual[0].Should().Be("Received 0");
            actual[1].Should().Be("Received 1");
            actual[2].Should().Be("Received 2");
            actual[3].Should().Be("Received 3");
            actual[4].Should().Be("Received 4");
            actual[5].Should().Be("Received 5");
            actual[6].Should().Be("Received 6");
            actual[7].Should().Be("Received 7");
            actual[8].Should().Be("Received 8");
            actual[9].Should().Be("Received 9");
            actual[10].Should().Be("Done");
        }
    }
}