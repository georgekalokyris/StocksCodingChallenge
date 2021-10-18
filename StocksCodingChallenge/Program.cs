using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksCodingChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManagement.RequestPath();


            Database db = new Database();
            db.AddStocksFromFile();
            StockResults stockResults = new StockResults(db);
            InvestmentStrategies investments = new InvestmentStrategies(stockResults);
            Print print = new Print(stockResults, investments);

            print.NumberOfStocks();
            Console.WriteLine("-----------------------------------------");
            print.PrintAllStocks();
            Console.WriteLine("-----------------------------------------");
            print.PrintHigherPrice();
            Console.WriteLine("-----------------------------------------");
            print.PrintLowerPrice();
            Console.WriteLine("-----------------------------------------");
            print.PrintHigherStock();
            Console.WriteLine("-----------------------------------------");
            print.PrintLowerStock();
            Console.WriteLine("-----------------------------------------");
            print.PrintStocksByPriceDesc();
            Console.WriteLine("-----------------------------------------");
            print.PrintStocksByPriceAsc();
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("end");
            Console.WriteLine("-----------------------------------------");
            print.BuyOneSellOnePrint();
            Console.WriteLine("-------------------------------------------");
            print.BuySecondLowerDayPrint();


            Console.ReadKey();

             
        }

    }

    public class Stock
    {
        public int Day { get; set; }
        public double Price { get; set; }
        public Stock(int Day, double Price)
        {
            this.Day = Day;
            this.Price = Price;
        }
    }



    public static class FileManagement
    {
     
        public static string filePath = "";
        public static string defaultPath = @"C:\ChallengeSampleDataSet1.txt";

        //TODO: Request path from user
        public static void RequestPath()
        {
            Console.WriteLine(@"Default Directory & File Name: [C:\ChallengeSampleDataSet1.txt]");
            Console.WriteLine("Press 'Enter' to continue with the default path and file name or enter a specific");

            string consoleReading = Console.ReadLine();

            filePath = consoleReading == "" ? defaultPath : consoleReading;

            Console.WriteLine($"Selected file is: {filePath}");
        }


    }

    public class Database
    {

        public List<Stock> stocks = new List<Stock>();
        public void AddStocksFromFile()
        {
            //TODO: This should be at file management
            string stockText ="";
            try
            {
                stockText = File.ReadAllText(FileManagement.filePath);
            }
            catch
            {
                Console.WriteLine("Error with path!");
                FileManagement.RequestPath();
                Console.WriteLine($"Selected file is: {FileManagement.filePath}");

            }
            

            try
            {
                string[] stockPrices = stockText.Split(',');

                foreach (var price in stockPrices)
                {
                    stocks.Add(AddStock(stocks.Count + 1, Convert.ToDouble(price)));
                }

                if (stockPrices.Length == 0)
                {
                    Console.WriteLine("No stocks were imported");
                    //Fix the logic here so whenever a file is read with just one record to request for the path again!
                    FileManagement.RequestPath();
                }
            }
            catch(Exception e)
            {
                //AddStocksFromFile();
            }
            
        }

        public static Stock AddStock(int day, double price)
        {
            return new Stock(day, price);
        }

        public void UpdateStock(Stock stock, int day, double price)
        {
            stock.Day = day;
            stock.Price = price;
        }

        public void DeleteStock(Stock stock)
        {
            stocks.Remove(stock);
        }
    }

    public class StockResults
    {
        public int TotalStocks() => db.stocks.Count();
        public double minPrice() => db.stocks.Select(x => x.Price).Min<double>();
        public double maxPrice() => db.stocks.Select(x => x.Price).Max<double>();

        public Stock StockWithHigherPrice()
        {
            return db.stocks.Where(x => x.Price == maxPrice()).First();
        }
        public Stock StockWithLowerPrice()
        {
            return db.stocks.Where(x => x.Price == minPrice()).First();
        }

        public List<Stock> StocksByPriceAsc()
        {
            return db.stocks.OrderBy(x => x.Price).ToList();
        }

        public List<Stock> StocksByPriceDesc()
        {
            return db.stocks.OrderByDescending(x => x.Price).ToList();
        }


        public Database db;
        
        public StockResults(Database database)
        {
            db = database;
        }

    }

    public class Print
    {
        public void NumberOfStocks()
        {
            Console.WriteLine($"Total number of stocks read from the database: {stockResults.TotalStocks()}");
        }
        public void PrintAllStocks()
        {
            Console.WriteLine("Printing all Stocks as read from data source");
            foreach (Stock stock in stockResults.db.stocks)
            {
                Console.WriteLine($"Stock Day: {stock.Day} - Stock Price: {stock.Price}");
            }
        }
        public void PrintLowerPrice()
        {
            Console.WriteLine($"The lower price is: {stockResults.minPrice()}");
        }

        public void PrintHigherPrice()
        {
            Console.WriteLine($"The highest price is: {stockResults.maxPrice()}");
        }

        public void PrintLowerStock()
        {
            Console.WriteLine($"Day with lower price: {stockResults.StockWithLowerPrice().Day}, Price on that day: {stockResults.StockWithLowerPrice().Price}"); 
        }

        public void PrintHigherStock()
        {
            Console.WriteLine($"Day with higher price: {stockResults.StockWithHigherPrice().Day}, Price on that day: {stockResults.StockWithHigherPrice().Price}");
        }

        public void PrintStocksByPriceAsc()
        {
            Console.WriteLine("Printing all stocks by Price Ascending \n ");
            foreach (var stock in stockResults.StocksByPriceAsc())
            {
                Console.WriteLine($"Price {stock.Price}, Day {stock.Day}");
            }
        }

        public void PrintStocksByPriceDesc()
        {
            Console.WriteLine("Printing all stocks by Price Descending \n ");

            foreach (var stock in stockResults.StocksByPriceDesc())
            {
                Console.WriteLine($"Price {stock.Price}, Day {stock.Day}");
            }
        }

        //TODO: Print results according to the requirement
        public void BuyOneSellOnePrint()
        {
            BuyStockSellStockProfit BSSS = investmentStrategies.BuyOneSellOne();
            Console.WriteLine("Printing results for the main strategy");
            Console.WriteLine($"Stock Purchase Day: {BSSS.buyStock.Day} || Stock Purchase Price: {BSSS.buyStock.Price}");
            Console.WriteLine($"Stock Sell Day: {BSSS.sellStock.Day} || Stock Sell Price: {BSSS.sellStock.Price}");
            Console.WriteLine($"Profit: {BSSS.Profit()}");
            Console.WriteLine($"\n{BSSS.buyStock.Day}({BSSS.buyStock.Price}),{BSSS.sellStock.Day}({BSSS.sellStock.Price})");
        }
        public void BuySecondLowerDayPrint()
        {
            BuyStockSellStockProfit BSSS = investmentStrategies.BuySecondLowerDay();
            Console.WriteLine("Printing results for the main strategy");
            Console.WriteLine($"Stock Purchase Day: {BSSS.buyStock.Day} || Stock Purchase Price: {BSSS.buyStock.Price}");
            Console.WriteLine($"Stock Sell Day: {BSSS.sellStock.Day} || Stock Sell Price: {BSSS.sellStock.Price}");
            Console.WriteLine($"Profit: {BSSS.Profit()}");
            Console.WriteLine($"\n{BSSS.buyStock.Day}({BSSS.buyStock.Price}),{BSSS.sellStock.Day}({BSSS.sellStock.Price})");
        }


        StockResults stockResults;
        InvestmentStrategies investmentStrategies;
        public Print(StockResults stockResults, InvestmentStrategies investmentStrategies = null)
        {
            this.stockResults = stockResults;
            this.investmentStrategies = investmentStrategies;
        }

    }

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
                for (int j = i+1; j < stockResults.db.stocks.Count(); j++) //Sell Day
                {
                    double diff = stockResults.db.stocks[j].Price - stockResults.db.stocks[i].Price;
                    
                    if (maxDiff < diff)
                    {
                        maxDiff = diff;
                        buyDay = i+1;
                        sellDay = j+1;
                    }
                }
            }


            Stock buyStock = stockResults.db.stocks[buyDay - 1];
            Stock sellStock = stockResults.db.stocks[sellDay - 1];

            BuyStockSellStockProfit BSSS = new BuyStockSellStockProfit(buyStock, sellStock);

            return BSSS;
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



    

