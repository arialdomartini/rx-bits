using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;

namespace ReactiveBits.WithRX
{
    internal class StockObserver : IDisposable
    {
        private readonly TextWriter _writer;
        private const decimal MaxChangeRatio = 0.1m;
        private readonly IDisposable _subscriptions;

        public StockObserver(StockObservable stockObservable, TextWriter writer)
        {
            _writer = writer;
            var priceChangedEvents = Observable.FromEventPattern<EventHandler<PriceChangedEventData>, PriceChangedEventData>(
                    h => stockObservable.PriceChanged += h,
                    h => stockObservable.PriceChanged -= h)
                .Select(e => e.EventArgs)
                .Synchronize();

            var drasticChanges = from priceChange in priceChangedEvents
                group priceChange by priceChange.QuoteSymbol
                into company
                from priceChangePair in company.Buffer(2, 1)
                let changeRatio = CalculateChangeRatio(priceChangePair)
                where changeRatio > MaxChangeRatio
                select new DrasticChange
                {
                    Symbol = company.Key,
                    ChangeRatio = changeRatio,
                    OldPrice = priceChangePair[0].Price,
                    NewPrice = priceChangePair[1].Price
                };

            _subscriptions = drasticChanges.Subscribe(OnDrasticChange);
        }

        public void Dispose()
        {
            _subscriptions.Dispose();
        }

        private void OnDrasticChange(DrasticChange change)
        {
            _writer.WriteLine(
                $"Stock: {change.Symbol} has changed price from {change.OldPrice} to {change.NewPrice}, that is a ratio of {change.ChangeRatio}");
        }

        private static decimal CalculateChangeRatio(IList<PriceChangedEventData> priceChangePair)
        {
            var oldPrice = priceChangePair[1].Price;
            var newPrice = priceChangePair[0].Price;

            return Math.Abs(oldPrice - newPrice) / newPrice;
        }
    }
}