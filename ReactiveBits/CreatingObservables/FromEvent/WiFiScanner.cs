using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using FluentAssertions;
using ReactiveBits.CreatingObservables.Handcrafted;
using Xunit;

namespace ReactiveBits.CreatingObservables.FromEvent
{
    public class WiFiScanner
    {
        public event NetworkFoundHandler NetworkFound;

        public void FindNetwork(string ssid)
        {
            NetworkFound(ssid);
        }
    }

    public delegate void NetworkFoundHandler(string ssid);

    public class WiFiScannerTest
    {

        [Fact]
        public void should_consume_the_events_in_the_classic_way()
        {
            var sut = new WiFiScanner();

            var ssids = new List<string>();
            NetworkFoundHandler myHandler = ssid => ssids.Add(ssid);
            sut.NetworkFound += myHandler;

            sut.FindNetwork("1");
            sut.FindNetwork("2");
            sut.FindNetwork("3");

            sut.NetworkFound -= myHandler;

            ssids[0] = "1";
            ssids[1] = "2";
            ssids[2] = "3";
        }


        [Fact]
        public void should_create_an_observable_using_FromEvent()
        {
            var sut = new WiFiScanner();
            var networks = Observable.FromEvent<NetworkFoundHandler, string>(
                h => sut.NetworkFound += h,
                h => sut.NetworkFound -= h
            );

            var result = new List<string>();

            using (networks.Subscribe(new StringObserver<string>(result)))
            {
                sut.FindNetwork("123");
                sut.FindNetwork("456");
            }

            result[0].Should().Be("Received 123");
            result[1].Should().Be("Received 456");
        }
    }
}