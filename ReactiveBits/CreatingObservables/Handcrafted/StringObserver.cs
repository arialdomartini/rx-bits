using System;
using System.Text;

namespace ReactiveBits.CreatingObservables.Handcrafted
{
    public class StringObserver<T> : IObserver<T>
    {
        private readonly StringBuilder _result;

        public StringObserver(StringBuilder result)
        {
            _result = result;
        }

        public void OnNext(T value)
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
}