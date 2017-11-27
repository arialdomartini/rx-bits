using System;
using System.Reactive.Disposables;
using System.Text;
using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;

namespace ReactiveBits
{
    public class CreatingObservables
    {
        public class NumberObservable : IObservable<int>
        {
            private readonly int _amount;

            public NumberObservable(int amount)
            {
                _amount = amount;
            }

            public IDisposable Subscribe(IObserver<int> observer)
            {
                for (var i = 0; i < _amount; i++)
                    observer.OnNext(i);
                observer.OnCompleted();

                return Disposable.Empty;
            }
        }
    }

    public class StringObserver : IObserver<int>
    {
        private readonly StringBuilder _result;

        public StringObserver(StringBuilder result)
        {
            _result = result;
        }

        public void OnNext(int value)
        {
            _result.AppendLine($"Received {value}");
        }

        public void OnError(Exception error)
        {
            _result.AppendLine("Got an errore");
        }

        public void OnCompleted()
        {
            _result.AppendLine("Done");
        }
    }

    public class NumberObservableTest
    {
        [Fact]
        public void should_return_all_the_items()
        {
            var observable = new CreatingObservables.NumberObservable(10);

            var result = new StringBuilder();
            var stringObserver = new StringObserver(result);

            using (observable.Subscribe(stringObserver))
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