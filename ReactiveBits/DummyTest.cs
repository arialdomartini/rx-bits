using FluentAssertions;
using Xunit;

namespace ReactiveBits
{
    public class DummyTest
    {
        [Fact]
        public void should_pass()
        {
            true.Should().Be(true);
        }
    }
}
