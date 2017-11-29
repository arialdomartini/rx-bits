using System.Collections.Generic;
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

            observable.Subscribe(new StringObserver<string>(new List<string>()));

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

            var sb = new List<string>();
            observable.Subscribe(new StringObserver<string>(sb));

            sb[0].Should().Be("OnNext(0)");
            sb[1].Should().Be("OnNext(1)");
            sb[2].Should().Be("OnNext(2)");
            sb[3].Should().Be("OnNext(3)");
            sb[4].Should().Be("OnNext(4)");
            sb[5].Should().Be("OnCompleted()");
        }
    }
}