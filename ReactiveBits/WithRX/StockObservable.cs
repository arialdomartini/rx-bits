using System;

namespace ReactiveBits.WithRX
{
    // It represents the source of stock information
    internal class StockObservable
    {
        // Observers can register this event with
        //
        //   stockTicker.PriceChangedEventData += OnStockTick;
        //
        // where the method OnStockTick must have the signature
        //
        //   void OnStockTick(object sender, PriceChangedEventData stockTick)
        public event EventHandler<PriceChangedEventData> PriceChanged;

        public void Notify(string quoteSymbol, int newPrice)
        {
            PriceChanged(this, new PriceChangedEventData(quoteSymbol, newPrice));
        }
    }

    // Holds the information about the stock change, notified by StockObservable
    internal class PriceChangedEventData
    {
        public PriceChangedEventData(string quoteSymbol, decimal price)
        {
            QuoteSymbol = quoteSymbol;
            Price = price;
        }

        public string QuoteSymbol { get; set; }
        public decimal Price { get; set; }
    }
}