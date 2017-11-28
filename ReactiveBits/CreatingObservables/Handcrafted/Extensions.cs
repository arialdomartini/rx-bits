using System;
using System.Collections.Generic;
using System.Text;

namespace ReactiveBits.CreatingObservables.Handcrafted
{
    public static class Extensions
    {
        public static IDisposable SubscribeString(this NumberObservable numberObservable, List<string> result)
        {
            var observer = new StringObserver<string>(result);
            return numberObservable.Subscribe(observer);
        }
    }
}