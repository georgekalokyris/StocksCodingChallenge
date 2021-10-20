namespace StocksCodingChallenge
{
    public struct BuyStockSellStockProfit
    {
        public Stock buyStock;
        public Stock sellStock;
        public double Profit() => sellStock.Price - buyStock.Price;
        public BuyStockSellStockProfit(Stock buyStock, Stock sellStock)
        {
            this.buyStock = buyStock;
            this.sellStock = sellStock;
        }
    }


}





