using System;
using System.IO;
using FluentAssertions;
using Xunit;

namespace ReactiveBits.WihoutRX
{
    public class StockMonitorTest : IDisposable
    {
        private readonly TextWriter _originalOut;

        public StockMonitorTest()
        {
            _originalOut = Console.Out;
        }

        [Fact]
        public void should_process_ticks()
        {
            using (TextWriter writer = new StringWriter())
            {
                Console.SetOut(writer);
                var stockTicker = new StockObservable();

                var sut = new StockObserver(stockTicker);

                stockTicker.Notify("yahoo", 100);
                stockTicker.Notify("amazon", 100);
                stockTicker.Notify("amazon", 125);
                stockTicker.Notify("yahoo", 200);

                writer.Flush();
                writer.ToString().Should().Contain("Stock: yahoo has changed price from 100 to 200, that is a ratio of 1");
                writer.ToString().Should().Contain("Stock: amazon has changed price from 100 to 125, that is a ratio of 0.25");
            }
        }

        public void Dispose()
        {
            Console.SetOut(_originalOut);
        }
    }
}