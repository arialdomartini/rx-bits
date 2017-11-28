using System;

namespace ReactiveBits.CreatingObservables.UsingObservableBase
{
    public class ChatConnection : IChatConnection
    {
        private readonly string _username;
        private readonly string _password;

        public ChatConnection(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public event Action<string> Received;
        public event Action Closed;
        public event Action<Exception> Error;

        public void Disconnect()
        {
            Closed();
        }

        public void SendMessage(string message)
        {
            Received(message);
        }
    }
}