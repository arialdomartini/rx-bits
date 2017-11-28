using System;

namespace ReactiveBits.CreatingObservables.UsingObservableBase
{
    public interface  IChatConnection
    {
        event Action<string> Received;
        event Action Closed;
        event Action<Exception> Error;

        void Disconnect();
    }
}