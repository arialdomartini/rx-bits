using System.Reactive.Disposables;
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
        public void creation_can_be_deferred()
        {
            var amount = 5;
            var created = false;
            var observable = Observable.Defer(() => Observable.Create<string>(observer =>
            {
                created = true;
                for (var i = 0; i < amount; i++)
                    observer.OnNext(i.ToString());
                observer.OnCompleted();
                return Disposable.Empty;
            }));

            created.Should().Be(false);

            observable.Subscribe(new StringObserver<string>(new StringBuilder()));

            created.Should().Be(true);
        }

        [Fact]
        public void should_create_an_observable_using_ObservableCreate()
        {
            var observable = Observable.Create<string>(observer =>
            {
                for (var i = 0; i < 5; i++)
                    observer.OnNext(i.ToString());
                observer.OnCompleted();
                return Disposable.Empty;
            });

            var sb = new StringBuilder();
            observable.Subscribe(new StringObserver<string>(sb));

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