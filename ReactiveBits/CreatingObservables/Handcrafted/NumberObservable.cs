using System;
using System.Reactive.Disposables;

namespace ReactiveBits.CreatingObservables.Handcrafted
{
    public class NumberObservable : IObservable<string>
    {
        private readonly int _amount;

        public NumberObservable(int amount)
        {
            _amount = amount;
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            for (var i = 0; i < _amount; i++)
                observer.OnNext(i.ToString());
            observer.OnCompleted();
            
            return Disposable.Empty;
        }
    }
}