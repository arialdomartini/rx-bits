using System;

namespace ReactiveBits.WihoutRX
{
    // The Observable. It represents the source of stock information
    internal class StockTicker
    {
        // Observers can register this event with
        //
        //   stockTicker.StockTick += OnStockTick;
        //
        // where the method OnStockTick must have the signature
        //
        //   void OnStockTick(object sender, StockTick stockTick)
        public event EventHandler<StockTick> StockTick;

        public void Notify(string quoteSymbol, int newPrice)
        {
            StockTick(this, new StockTick(quoteSymbol, newPrice));
        }
    }

    // Holds the information about the stock change, notified by StockTicker
    internal class StockTick
    {
        public StockTick(string quoteSymbol, decimal newPrice)
        {
            QuoteSymbol = quoteSymbol;
            NewPrice = newPrice;
        }

        public string QuoteSymbol { get; set; }
        public decimal NewPrice { get; set; }
    }
}