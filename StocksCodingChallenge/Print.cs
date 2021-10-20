using System;

namespace StocksCodingChallenge
{
    public class Print
    {
        public void NumberOfStocks()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Total number of stocks read from the file: {stockResults.TotalStocks()}");
            Console.ResetColor();
        }
        public void PrintAllStocks()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Printing all stocks with the default order");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            foreach (Stock stock in stockResults.db.stocks)
            {
                Console.WriteLine($"Stock[Day: {stock.Day} - Stock Price: {stock.Price}]");
            }
            Console.ResetColor();
        }

        public void PrintLowerPrice()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"The lower price is: {stockResults.minPrice()}");
            Console.ResetColor();
        }

        public void PrintHigherPrice()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"The highest price is: {stockResults.maxPrice()}");
            Console.ResetColor();
        }

        public void PrintLowerStock()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Day with lower price: {stockResults.StockWithLowerPrice().Day}, Price on that day: {stockResults.StockWithLowerPrice().Price}");
            Console.ResetColor();
        }

        public void PrintHigherStock()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Day with higher price: {stockResults.StockWithHigherPrice().Day}, Price on that day: {stockResults.StockWithHigherPrice().Price}");
            Console.ResetColor();
        }

        public void PrintStocksByPriceAsc()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Printing all stocks by Price Ascending \n ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            foreach (var stock in stockResults.StocksByPriceAsc())
            {
                Console.WriteLine($"Price {stock.Price}, Day {stock.Day}");
            }
            Console.ResetColor();

        }

        public void PrintStocksByPriceDesc()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Printing all stocks by Price Descending\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            foreach (var stock in stockResults.StocksByPriceDesc())
            {
                Console.WriteLine($"Price {stock.Price}, Day {stock.Day}");
            }
            Console.ResetColor();
        }

        public void BuyOneSellOnePrint()
        {
            BuyStockSellStockProfit BSSS = investmentStrategies.BuyOneSellOne();
            if (BSSS.Profit == 0)
            {
                Console.WriteLine("There is no profitable strategy");
            }
            else if (BSSS.buyStock != null && BSSS.sellStock != null)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Printing results for the main strategy");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Stock Purchase Day: {BSSS.buyStock.Day} || Stock Purchase Price: {BSSS.buyStock.Price}");
                Console.WriteLine($"Stock Sell Day: {BSSS.sellStock.Day} || Stock Sell Price: {BSSS.sellStock.Price}");
                Console.WriteLine($"Profit: {BSSS.Profit}");
                Console.WriteLine($"\n{BSSS.buyStock.Day}({BSSS.buyStock.Price}),{BSSS.sellStock.Day}({BSSS.sellStock.Price})");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("The selected strategy cannot be implemented");
            }

        }
        public void BuySecondLowerDayPrint()
        {
            BuyStockSellStockProfit BSSS = investmentStrategies.BuySecondLowerDay();

            if (BSSS.Profit == 0)
            {
                Console.WriteLine("There is no profitable strategy");
            }
            else if (BSSS.buyStock != null && BSSS.sellStock != null)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Printing results for the alternative strategy");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Stock Purchase Day: {BSSS.buyStock.Day} || Stock Purchase Price: {BSSS.buyStock.Price}");
                Console.WriteLine($"Stock Sell Day: {BSSS.sellStock.Day} || Stock Sell Price: {BSSS.sellStock.Price}");
                Console.WriteLine($"Profit: {BSSS.Profit}");
                Console.WriteLine($"\n{BSSS.buyStock.Day}({BSSS.buyStock.Price}),{BSSS.sellStock.Day}({BSSS.sellStock.Price})");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("The selected strategy cannot be implemented");
            }
        }

        StockResults stockResults;
        InvestmentStrategies investmentStrategies;
        public Print(StockResults stockResults, InvestmentStrategies investmentStrategies = null)
        {
            this.stockResults = stockResults;
            this.investmentStrategies = investmentStrategies;
        }

    }


}





