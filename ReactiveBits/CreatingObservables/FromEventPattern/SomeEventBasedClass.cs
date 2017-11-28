using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FluentAssertions;
using ReactiveBits.CreatingObservables.Handcrafted;
using Xunit;

namespace ReactiveBits.CreatingObservables.FromEventPattern
{
    public class SomeEventBasedClass
    {
        public event MyHandler Sent;

        public void Send(string message)
        {
            Sent(this, new MyHandlerArgs {SomeField = message});
        }
    }

    public delegate void MyHandler(object sender, MyHandlerArgs args);

    public class MyHandlerArgs
    {
        public string SomeField { get; set; }
    }

    public class EventConsumerTest
    {
        [Fact]
        public void should_consume_event_in_the_classic_way()
        {
            var sut = new SomeEventBasedClass();

            var messages = new List<string>();
            MyHandler myHandler = (sender, args) => messages.Add(args.SomeField);

            sut.Sent += myHandler;

            sut.Send("message 1");
            sut.Send("message 2");
            sut.Send("message 3");

            messages[0].Should().Be("message 1");
            messages[1].Should().Be("message 2");
            messages[2].Should().Be("message 3");

            sut.Sent -= myHandler;
        }

        [Fact]
        public void should_consume_the_same_events_as_a_stream()
        {
            var sut = new SomeEventBasedClass();
            var stream = Observable.FromEventPattern<MyHandler, MyHandlerArgs>(
                handler => sut.Sent += handler, 
                handler => sut.Sent -= handler);

            var sb = new StringBuilder();
            using (var subscription = stream.Subscribe(new MyHandlerArgsStringObserver(sb)))
            {
                sut.Send("message 1");
                sut.Send("message 2");
                sut.Send("message 3");
            }

            var messages = Regex.Split(sb.ToString(), "\r\n");
            messages[0].Should().Be("message 1");
            messages[1].Should().Be("message 2");
            messages[2].Should().Be("message 3");
        }
    }

    public class MyHandlerArgsStringObserver : IObserver<EventPattern<MyHandlerArgs>>
    {
        private readonly StringBuilder _sb;

        public MyHandlerArgsStringObserver(StringBuilder sb)
        {
            _sb = sb;
        }

        public void OnNext(EventPattern<MyHandlerArgs> value)
        {
            _sb.AppendLine(value.EventArgs.SomeField);
        }

        public void OnError(Exception error)
        {
            _sb.AppendLine("Got an error");
        }

        public void OnCompleted()
        {
            _sb.AppendLine("Done");
        }
    }
}