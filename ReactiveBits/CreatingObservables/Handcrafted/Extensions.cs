using System;
using System.Text;

namespace ReactiveBits.CreatingObservables.Handcrafted
{
    public static class Extensions
    {
        public static IDisposable SubscribeString(this NumberObservable numberObservable, StringBuilder result)
        {
            IObserver<int> observer = new StringObserver(result);
            return numberObservable.Subscribe(observer);
        }
    }
}