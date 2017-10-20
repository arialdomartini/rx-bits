namespace ReactiveBits.WihoutRX
{
    internal class StockInfo
    {
        public StockInfo(string symbol, decimal price)
        {
            Symbol = symbol;
            Price = price;
        }

        public string Symbol { get; set; }
        public decimal Price { get; set; }
    }
}