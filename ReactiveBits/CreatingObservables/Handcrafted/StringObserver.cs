using System;
using System.Collections.Generic;
using System.Text;

namespace ReactiveBits.CreatingObservables.Handcrafted
{
    public class StringObserver<T> : IObserver<T>
    {
        private readonly List<string> _result;

        public StringObserver(List<string> result)
        {
            _result = result;
        }

        public void OnNext(T value)
        {
            _result.Add($"Received {value}");
        }

        public void OnError(Exception error)
        {
            _result.Add("Got an errore");
        }

        public void OnCompleted()
        {
            _result.Add("Done");
        }
    }
}