using System.Collections.Generic;
using System.Reactive.Linq;
using FluentAssertions;
using ReactiveBits.CreatingObservables.Handcrafted;
using Xunit;

namespace ReactiveBits.CreatingObservables.FromEvent
{
    public class WiFiScanner
    {
        public event NetworkFoundHandler NetworkFound;
        public event ExtendedNetworkFoundHandler ExtendedNetworkFound;


        public void FindNetwork(string ssid)
        {
            NetworkFound(ssid);
        }

        public void FindExtendedNetwork(string ssid, int strength)
        {
            ExtendedNetworkFound(ssid, strength);
        }
    }


    public delegate void NetworkFoundHandler(string ssid);
    public delegate void ExtendedNetworkFoundHandler(string ssid, int strength);

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

            result[0].Should().Be("OnNext(123)");
            result[1].Should().Be("OnNext(456)");
        }

        [Fact]
        public void should_consume_extended_event_in_the_classic_way()
        {
            var sut = new WiFiScanner();

            var networks = new List<Info>();
            ExtendedNetworkFoundHandler sutOnExtendedNetworkFound = (ssid, strength) =>
            {
                networks.Add(new Info {Ssid = ssid, Strength = strength});
            };

            sut.ExtendedNetworkFound += sutOnExtendedNetworkFound;

            sut.FindExtendedNetwork("one", 1);
            sut.FindExtendedNetwork("two", 2);

            networks[0].ShouldBeEquivalentTo(new Info {Ssid = "one", Strength = 1});
            networks[1].ShouldBeEquivalentTo(new Info {Ssid = "two", Strength = 2});

            sut.ExtendedNetworkFound -= sutOnExtendedNetworkFound;
        }

        [Fact]
        public void should_create_an_extended_observable_using_FromEvent()
        {
            var sut = new WiFiScanner();

            var networks = Observable.FromEvent<ExtendedNetworkFoundHandler, Info>(
                handler => ((ssid, strength) => handler(new Info() {Ssid = ssid, Strength = strength})), 
                handler => { sut.ExtendedNetworkFound += handler; }, 
                handler => { sut.ExtendedNetworkFound -= handler; });

            var sb = new List<string>();
            using (networks
                .Select(n => $"{n.Ssid} ({n.Strength})")
                .Subscribe(new StringObserver<string>(sb)))
            {
                sut.FindExtendedNetwork("one", 1);
                sut.FindExtendedNetwork("two", 2);
            }

            sb[0].Should().Be("OnNext(one (1))");
            sb[1].Should().Be("OnNext(two (2))");

        }


        public class Info
        {
            public string Ssid { get; set; }
            public int Strength { get; set; }
        }
    }
}