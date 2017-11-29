using System.Collections.Generic;
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

            var result = new List<string>();
            var stringObserver = new StringObserver<string>(result);

            
            using (var subscription = observableConnection.Subscribe(stringObserver))
            {
                chatConnection.SendMessage("Hello");
                chatConnection.SendMessage("World");
                chatConnection.Disconnect();
            }

            result[0].Should().Be("OnNext(Hello)");
            result[1].Should().Be("OnNext(World)");
            result[2].Should().Be("OnCompleted()");
        }
    }
}