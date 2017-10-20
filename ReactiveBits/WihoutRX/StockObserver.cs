using System;
using System.Collections.Generic;

namespace ReactiveBits.WihoutRX
{
    /// Subscribe to price changes hooking up to the PriceChangedEventData event
    internal class StockObserver : IDisposable
    {
        private const decimal MaxChangeRatio = 0.1m;
        private readonly Dictionary<string, StockInfo> _stockInfos = new Dictionary<string, StockInfo>();
        private readonly StockObservable _stockObservable;
        private readonly object _stockTickLock = new object();

        public StockObserver(StockObservable stockObservable)
        {
            _stockObservable = stockObservable;

            // Subscribe to the event
            _stockObservable.PriceChanged += OnPriceChanged;
        }

        public void Dispose()
        {
            // Unsubscribe to the event
            _stockObservable.PriceChanged -= OnPriceChanged;
            _stockInfos.Clear();
        }

        // This is the PriceChanged Event Handler
        private void OnPriceChanged(object sender, PriceChangedEventData priceChangedEventData)
        {
            var quoteSymbol = priceChangedEventData.QuoteSymbol;
            var newPrice = priceChangedEventData.NewPrice;

            var newStockInfo = new StockInfo(quoteSymbol, newPrice);

            OnPriceChanged(newStockInfo);
        }

        // The critical section
        private void OnPriceChanged(StockInfo newStockInfo)
        {
            lock (_stockTickLock)
            {
                if (HasNeverBeenProcessed(newStockInfo.Symbol))
                    SaveNew(newStockInfo);
                else
                    Process(newStockInfo);
            }
        }

        private void Process(StockInfo newStockInfo)
        {
            var quoteSymbol = newStockInfo.Symbol;
            var previousStockInfo = _stockInfos[quoteSymbol];

            var oldPrice = previousStockInfo.Price;
            var newPrice = newStockInfo.Price;

            var priceDifference = newPrice - oldPrice;
            var changeRatio = Math.Abs(priceDifference / oldPrice);

            if (changeRatio > MaxChangeRatio)
                Console.WriteLine(
                    $"Stock: {quoteSymbol} has changed price from {oldPrice} to {newPrice}, that is a ratio of {changeRatio}");

            Update(newStockInfo);
        }

        private bool HasNeverBeenProcessed(string quoteSymbol)
        {
            return !HasAlreadyBeenProcessed(quoteSymbol);
        }

        private bool HasAlreadyBeenProcessed(string quoteSymbol)
        {
            return _stockInfos.ContainsKey(quoteSymbol);
        }

        private StockInfo Update(StockInfo stockInfo)
        {
            return _stockInfos[stockInfo.Symbol] = stockInfo;
        }

        private void SaveNew(StockInfo stockInfo)
        {
            _stockInfos.Add(stockInfo.Symbol, stockInfo);
        }
    }
}