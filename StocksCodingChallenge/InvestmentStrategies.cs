using System;
using System.Linq;

namespace StocksCodingChallenge
{
    public class InvestmentStrategies
    {
        //Default requirement for buying a single stock and then selling it at the highest possible price
        public BuyStockSellStockProfit BuyOneSellOne()
        {
            return BuyOneSellOneFromDay(0);
        }
        public BuyStockSellStockProfit BuyOneSellOneFromDay(int startBuyDay)
        {
            double maxDiff = 0;
            int buyDay = 0;
            int sellDay = 0;

            for (int i = startBuyDay; i < stockResults.db.stocks.Count(); i++) //Buy Day
            {
                for (int j = i + 1; j < stockResults.db.stocks.Count(); j++) //Sell Day
                {
                    double diff = stockResults.db.stocks[j].Price - stockResults.db.stocks[i].Price;

                    if (maxDiff < diff)
                    {
                        maxDiff = diff;
                        buyDay = i;
                        sellDay = j;
                    }
                }
            }
            Stock buyStock = stockResults.db.stocks[buyDay];
            Stock sellStock = stockResults.db.stocks[sellDay];

            BuyStockSellStockProfit BSSS = new BuyStockSellStockProfit(buyStock, sellStock);

            return BSSS;

        }

        /*
           Now let's take the scenario of not being able to afford the stock on the day of the lowest price.
           So, we will have to purchase the stock on any subsequent day with the highest profit.

           This strategy similar to the first will:
           Find the best possible days to buy and sell the stock(i.e. maximize the profit)
           Given that the buy day must be after the buy day returned by the initial strategy (i.e. BuyOneSellOne)

         */

        public BuyStockSellStockProfit BuySecondLowerDay()
        {
            return BuyOneSellOneFromDay(BuyOneSellOne().buyStock.Day + 1);
        }


        StockResults stockResults;
        public InvestmentStrategies(StockResults stockResults)
        {
            this.stockResults = stockResults;
        }


    }


}





