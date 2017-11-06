using FluentAssertions;
using Xunit;

namespace ReactiveBits.FunctionalProgramming.Examples.LazyLoading
{
    public static class Global
    {
        public static bool HeavyClassCreated = false;
    }

    public class HeavyClass
    {
        // This class takes a lot of time to be create
        public HeavyClass()
        {
            Global.HeavyClassCreated = true;
        }

        public string GetAString()
        {
            return "foo";
        }
    }

    public class LightClassWithoutFunc
    {
        private HeavyClass _heavyClass;

        private HeavyClass HeavyClass => _heavyClass ?? (_heavyClass = new HeavyClass());

        public string SomeMethod()
        {
            return HeavyClass.GetAString();
        }
     }

    public class DeferredInvocationTest
    {
        [Fact]
        public void should_defer_the_creation_of_the_heavy_class()
        {
            var sut = new LightClassWithoutFunc();

            Global.HeavyClassCreated.Should().Be(false);

            sut.SomeMethod();

            Global.HeavyClassCreated.Should().Be(true);
        }
    }
}