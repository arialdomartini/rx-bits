using System;
using System.Text;

namespace ReactiveBits.CreatingObservables.Handcrafted
{
    public static class Extensions
    {
        public static IDisposable SubscribeString(this NumberObservable numberObservable, StringBuilder result)
        {
            var observer = new StringObserver(result);
            return numberObservable.Subscribe(observer);
        }
    }
}