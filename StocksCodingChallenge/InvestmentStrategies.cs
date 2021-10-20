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
            try
            {
                for (int i = startBuyDay; i < stockResults.db.stocks.Count(); i++) //Buy Day
                {
                    for (int j = i + 1; j < stockResults.db.stocks.Count(); j++) //Sell Day
                    {
                        double diff = stockResults.db.stocks[j].Price - stockResults.db.stocks[i].Price;

                        if (maxDiff < diff)
                        {
                            maxDiff = diff;
                            buyDay = i + 1;
                            sellDay = j + 1;
                        }
                    }
                }

                Stock buyStock = stockResults.db.stocks[buyDay - 1];
                Stock sellStock = stockResults.db.stocks[sellDay - 1];

                BuyStockSellStockProfit BSSS = new BuyStockSellStockProfit(buyStock, sellStock);

                return BSSS;
            }
            catch
            {
                //TODO: Add a validation so this runs only if there are at least 2 stocks in the database
                Console.WriteLine("Investment cannot be implemented with one stock");
                FileManagement.RequestPath();
            }


            return new BuyStockSellStockProfit(null, null);
        }
        //TODO: Update the following description 
        /* Now let's take the scenario of not being able to afford the stock on the day of the lowest price.
           So, we will have to purchase the stock on the next possible day with the lowest price.

           This strategy similar to the first will:
         * Buy a Stock in one of the following days the lowest price was observed
         * Sell the stock on the day the highest possible price was observerd

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





