using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using FluentAssertions;
using ReactiveBits.CreatingObservables.Handcrafted;
using Xunit;

namespace ReactiveBits.FromObservable
{
    public class ToOtherStructures
    {
        [Fact]
        public void should_convert_an_observable_to_a_dictionary()
        {
            var words = new List<string>{"123", "1234", "12345", "12345678"};
            var observable = words
                                .ToObservable()
                                .ToDictionary(w => w.Length)
                                .Select(d => string.Join(",", d));

            var result = new List<string>();

            using(observable.Subscribe(new StringObserver<string>(result))) { }

            result[0].Should().Be("OnNext([3, 123],[4, 1234],[5, 12345],[8, 12345678])");
        }
    }
}