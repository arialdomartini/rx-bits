using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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

        [Fact]
        public void should_merge_old_messages_with_live_ones()
        {
            var oldMessages = new List<string>() { "old 1", "old 2"};
            var chatConnection = new ChatConnection("username", "password");
            var connection = new ObservableConnection(chatConnection);

            var allMessages = oldMessages.ToObservable()
                                .Concat(connection);

            var result = new List<string>();
            using (allMessages.Subscribe(new StringObserver<string>(result)))
            {
                chatConnection.SendMessage("new 1");
                chatConnection.SendMessage("new 2");
                chatConnection.Disconnect();
            }

            result[0].Should().Be("OnNext(old 1)");
            result[1].Should().Be("OnNext(old 2)");
            result[2].Should().Be("OnNext(new 1)");
            result[3].Should().Be("OnNext(new 2)");
            result[4].Should().Be("OnCompleted()");
        }
    }
}