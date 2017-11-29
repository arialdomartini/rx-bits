using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using FluentAssertions;
using ReactiveBits.CreatingObservables.Handcrafted;
using Xunit;

namespace ReactiveBits.CreatingObservables.FromEnumerables
{
    public static class Extension
    {
        public static IObservable<T> MakeMeObservable<T>(this List<T> list)
        {
            return Observable.Create<T>(observer =>
            {
                foreach (var item in list)
                {
                    observer.OnNext(item);
                }
                observer.OnCompleted();
                return Disposable.Empty;
            });
        }
    }

    public class MakeMeObservableTest
    {
        [Fact]
        public void should_convert_a_collection_to_observable()
        {
            var ints = new List<int> {1, 2, 3};

            var streamOfInts = ints.MakeMeObservable();

            var results = new List<string>();
            streamOfInts.Subscribe(new StringObserver<int>(results));

            results[0].Should().Be("Received 1");
            results[1].Should().Be("Received 2");
            results[2].Should().Be("Received 3");
            results[3].Should().Be("Done");
        }

        [Fact]
        public void should_convert_a_collection_to_observable_using_the_builtin_function()
        {
            var ints = new List<int> {1, 2, 3};

            var streamOfInts = ints.ToObservable();


            var results = new List<string>();
            streamOfInts.Subscribe(new StringObserver<int>(results));

            results[0].Should().Be("Received 1");
            results[1].Should().Be("Received 2");
            results[2].Should().Be("Received 3");
            results[3].Should().Be("Done");
        }
    }
}