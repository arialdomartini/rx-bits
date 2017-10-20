using System;
using System.Collections.Generic;

namespace ReactiveBits.WihoutRX
{
    /// <summary>
    ///     Listens to stock changes hooking up to the StockTick event
    /// </summary>
    internal class StockMonitor : IDisposable
    {
        private const decimal MaxChangeRatio = 0.1m;
        private readonly Dictionary<string, StockInfo> _previousStockInfos = new Dictionary<string, StockInfo>();
        private readonly StockTicker _stockTicker;

        public StockMonitor(StockTicker stockTicker)
        {
            _stockTicker = stockTicker;
            _stockTicker.StockTick += OnStockTick;
        }

        public void Dispose()
        {
            _stockTicker.StockTick -= OnStockTick;
            _previousStockInfos.Clear();
        }

        private void OnStockTick(object sender, StockTick stockTick)
        {
            var quoteSymbol = stockTick.QuoteSymbol;
            var newPrice = stockTick.NewPrice;

            if (!_previousStockInfos.ContainsKey(quoteSymbol))
                _previousStockInfos.Add(quoteSymbol, new StockInfo(quoteSymbol, newPrice));

            var previousStockInfo = _previousStockInfos[quoteSymbol];
            var oldPrice = previousStockInfo.Price;

            var priceDifference = newPrice - oldPrice;
            decimal changeRatio = Math.Abs(priceDifference / oldPrice);

            if (changeRatio > MaxChangeRatio)
                Console.WriteLine(
                    $"Stock: {quoteSymbol} has changed price from {oldPrice} to {newPrice}, that is a ratio of {changeRatio}");
        }
    }
}