using System;
using FluentAssertions;
using Xunit;

namespace ReactiveBits.FunctionalProgramming.LazyLoading
{
    public class DeferredInvocationTest
    {
        [Fact]
        public void should_defer_the_creation_of_the_heavy_class()
        {
            var sut = new LightClassWithoutFunc();

            sut.HeavyClassInstance.Should().BeNull();

            sut.SomeMethod();

            sut.HeavyClassInstance.Should().NotBeNull();
        }

        [Fact]
        public void should_defer_the_creation_using_a_func()
        {
            var created = false;
            Func<HeavyClass> heavyClassFactory = () =>
            {
                created = true;
                return new HeavyClass();
            };

            var sut = new LightClassWithFunc(heavyClassFactory);

            created.Should().Be(false);

            sut.SomeMethod();

            created.Should().Be(true);
        }

        [Fact]
        public void should_defer_the_creation_using_Lazy()
        {
            var created = false;
            var lazyHeavyClass = new Lazy<HeavyClass>(() =>
            {
                created = true;
                return new HeavyClass();
            });

            var sut = new LightClassWithLazy(lazyHeavyClass);

            created.Should().Be(false);

            sut.SomeMethod();

            created.Should().Be(true);
        }

        [Fact]
        public void factory_based_on_func_is_invoked_each_time()
        {
            var count = 0;
            Func<HeavyClass> heavyClassFactory = () =>
            {
                count++;
                return new HeavyClass();
            };
            var sut = new LightClassWithFunc(heavyClassFactory);

            sut.SomeMethod();
            sut.SomeMethod();
            sut.SomeMethod();

            count.Should().Be(3);
        }


        [Fact]
        public void lazy_should_invoke_only_once()
        {
            var numberOfInvocations = 0;
            var lazyHeavyClass = new Lazy<HeavyClass>(() =>
            {
                numberOfInvocations++;
                return new HeavyClass();
            });

            var sut = new LightClassWithLazy(lazyHeavyClass);

            sut.SomeMethod();
            sut.SomeMethod();
            sut.SomeMethod();

            numberOfInvocations.Should().Be(1);
        }
    }
}