﻿using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FluentAssertions;
using ReactiveBits.CreatingObservables.Handcrafted;
using Xunit;

namespace ReactiveBits.CreatingObservables.WithObservableCreate
{
    public class NumberObservableTest
    {
        [Fact]
        public void should_create_an_observable_using_ObservableCreate()
        {
            var observable = Observable.Create<string>( observer =>
            {
                for (var i = 0; i < 5; i++)
                {
                    observer.OnNext(i.ToString());
                }
                observer.OnCompleted();
                return Disposable.Empty;
            });

            var sb = new StringBuilder();
            observable.Subscribe(new StringObserver(sb));

            var actual = Regex.Split(sb.ToString(), "\r\n");

            actual[0].Should().Be("Received 0");
            actual[1].Should().Be("Received 1");
            actual[2].Should().Be("Received 2");
            actual[3].Should().Be("Received 3");
            actual[4].Should().Be("Received 4");
            actual[5].Should().Be("Done");
        }
    }
}