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

        public StockObserver(StockObservable stockObservable)
        {
            _stockObservable = stockObservable;
            _stockObservable.PriceChanged += OnPriceChanged;
        }

        public void Dispose()
        {
            _stockObservable.PriceChanged -= OnPriceChanged;
            _stockInfos.Clear();
        }

        private void OnPriceChanged(object sender, PriceChangedEventData priceChangedEventData)
        {
            var quoteSymbol = priceChangedEventData.QuoteSymbol;
            var newPrice = priceChangedEventData.NewPrice;

            if (!_stockInfos.ContainsKey(quoteSymbol))
                _stockInfos.Add(quoteSymbol, new StockInfo(quoteSymbol, newPrice));

            var previousStockInfo = _stockInfos[quoteSymbol];
            var oldPrice = previousStockInfo.Price;

            var priceDifference = newPrice - oldPrice;
            decimal changeRatio = Math.Abs(priceDifference / oldPrice);

            if (changeRatio > MaxChangeRatio)
                Console.WriteLine(
                    $"Stock: {quoteSymbol} has changed price from {oldPrice} to {newPrice}, that is a ratio of {changeRatio}");
        }
    }
}