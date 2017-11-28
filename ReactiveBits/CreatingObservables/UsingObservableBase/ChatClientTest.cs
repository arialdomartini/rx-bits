using System.Text;
using System.Text.RegularExpressions;
using FluentAssertions;
using ReactiveBits.CreatingObservables.Handcrafted;
using Xunit;

namespace ReactiveBits.CreatingObservables.UsingObservableBase
{
    public class ChatClientTest
    {
        [Fact]
        public void should_consume_a_chat()
        {
            var chatConnection = new ChatConnection("username", "password");

            var observableConnection = new ObservableConnection(chatConnection);

            var sb = new StringBuilder();
            var stringObserver = new StringObserver<string>(sb);

            
            using (var subscription = observableConnection.Subscribe(stringObserver))
            {
                chatConnection.SendMessage("Hello");
                chatConnection.SendMessage("World");
                chatConnection.Disconnect();
            }

            var actual = Regex.Split(sb.ToString(), "\r\n");

            actual[0].Should().Be("Received Hello");
            actual[1].Should().Be("Received World");
            actual[2].Should().Be("Done");
        }
    }
}