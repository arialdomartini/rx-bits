using System;
using System.Reactive.Disposables;

namespace ReactiveBits.CreatingObservables.Handcrafted
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