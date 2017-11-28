using System;
using System.Text;

namespace ReactiveBits.CreatingObservables.Handcrafted
{
    public class StringObserver : IObserver<string>
    {
        private readonly StringBuilder _result;

        public StringObserver(StringBuilder result)
        {
            _result = result;
        }

        public void OnNext(string value)
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