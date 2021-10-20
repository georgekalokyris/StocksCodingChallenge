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
            App.Start();
        }

    }

    public static class App
    {
        public static void Start()
        {
            About();
            FileManagement.RequestPath();

            FileManagement.PrintPath();

            Database db = new Database();
            db.AddStocksFromFile();

            StockResults stockResults = new StockResults(db);

            InvestmentStrategies investmentStr = new InvestmentStrategies(stockResults);

            Print print = new Print(stockResults, investmentStr);

            MainMenu(stockResults, investmentStr, print);
        }

        public static void MainMenu(StockResults sr, InvestmentStrategies ins, Print pr)
        {
            MainMenuOptions();

            string menuOption = Console.ReadLine();

            while (!(menuOption == "1" || menuOption == "2" || menuOption == "3" || menuOption == "4"))
            {
                MainMenuOptions();
                menuOption = Console.ReadLine();
            }

            switch (menuOption)
            {
                case "1":
                    ImplementMainStrategy(pr, ins, sr);
                    break;
                case "2":
                    ImplementAlternativeStrategy(pr, ins, sr);
                    break;
                case "3":
                    PrintMenu(pr, sr, ins);
                    break;
                case "4":
                    About();
                    MainMenu(sr, ins, pr);
                    break;
                default:
                    break;
            }

        }

        public static void MainMenuOptions()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\t\tPlease select one of the following options:\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t1 - Implement Default Investment Strategy");
            Console.WriteLine("\t\t2 - Implement Alternative Investment Strategy");
            Console.WriteLine("\t\t3 - Dataset views");
            Console.WriteLine("\t\t4 - View the author's details");
            Console.ResetColor();
        }

        public static void ImplementMainStrategy(Print print, InvestmentStrategies inv, StockResults sr)
        {
            print.BuyOneSellOnePrint();
            ReturnToMainMenuOrExit(sr, inv, print);
        }

        public static void ImplementAlternativeStrategy(Print print, InvestmentStrategies inv, StockResults sr)
        {
            print.BuySecondLowerDayPrint();
            ReturnToMainMenuOrExit(sr, inv, print);
        }

        public static void PrintMenu(Print print, StockResults sr, InvestmentStrategies ins)
        {
            PrintMenuOptions();
            string selectedOption = "";

            while (!(selectedOption == "1" || selectedOption == "2" || selectedOption == "3" || selectedOption == "4" || selectedOption == "5" ||
                     selectedOption == "6" || selectedOption == "7" || selectedOption == "8" || selectedOption == "9"))
            {
                selectedOption = Console.ReadLine();
            }

            bool returnToMainMenu = false;

            switch (selectedOption)
            {
                case "1":
                    print.NumberOfStocks();
                    break;
                case "2":
                    print.PrintAllStocks();
                    break;
                case "3":
                    print.PrintLowerPrice();
                    break;
                case "4":
                    print.PrintHigherPrice();
                    break;
                case "5":
                    print.PrintLowerStock();
                    break;
                case "6":
                    print.PrintHigherStock();
                    break;
                case "7":
                    print.PrintStocksByPriceAsc();
                    break;
                case "8":
                    print.PrintStocksByPriceDesc();
                    break;
                case "9":
                    returnToMainMenu = true;
                    break;
            }

            if (returnToMainMenu) MainMenu(sr, ins, print);

            ReturnToPrintOrExit(print, sr, ins);
        }

        public static void ReturnToPrintOrExit(Print print, StockResults sr, InvestmentStrategies ins)
        {
            Console.WriteLine("Press 'b' to return or any key to exit");
            string option = Console.ReadLine();
            if (option == "b") PrintMenu(print, sr, ins);
        }

        public static void ReturnToMainMenuOrExit(StockResults sr, InvestmentStrategies ins, Print pr)
        {
            Console.WriteLine("Press 'b' to return or any key to exit");
            string option = Console.ReadLine();
            if (option == "b") MainMenu(sr, ins, pr);
        }


        public static void PrintMenuOptions()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\t\tPlease select one of the following view options:");
            Console.ResetColor();


            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("\n\t\t1 - Total number of imported stocks");
            Console.WriteLine("\t\t2 - All stocks: Default order");
            Console.WriteLine("\t\t3 - Lower price in the dataset");
            Console.WriteLine("\t\t4 - Higher price in the dataset");
            Console.WriteLine("\t\t5 - Stock with the lower price");
            Console.WriteLine("\t\t6 - Stock with the higher price");
            Console.WriteLine("\t\t7 - All stocks:Order by price ascending");
            Console.WriteLine("\t\t8 - All stocks:Order by price descending");

            Console.WriteLine("\n\t\t9 - Return to the Main Menu");

            Console.ResetColor();

        }

        public static void About()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\t\t Pre-Interview Coding Challenge - Computershare \t\t");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Created by: George Kalokyris - www.georgekalok.com - kalokirisg@gmail.com");
            Console.ResetColor();
            Console.WriteLine("\t\t\tPress 'Enter' to continue");
            Console.ReadLine();
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
        public static string stockText = "";

        //TODO: Customise this better with colors
        public static void RequestPath()
        {

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write("Press");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("'Enter'");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("to continue with the default path and file name or enter a valid path");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"The default directory and file Name is [C:\ChallengeSampleDataSet1.txt]");
            Console.ResetColor();

            string consoleReading = Console.ReadLine();

            filePath = consoleReading == "" ? defaultPath : consoleReading;

            try
            {
                stockText = File.ReadAllText(FileManagement.filePath);
            }
            catch
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nThe provided path or file are incorrect\n");
                Console.ResetColor();

                //If there is an exception request for the path again
                FileManagement.RequestPath();
            }


        }

        public static void PrintPath()
        {
            Console.Write($"Selected file is: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(FileManagement.filePath + "\n");
            Console.ResetColor();
        }


    }

    public class Database
    {

        public List<Stock> stocks = new List<Stock>();
        public string[] stockPrices;
        public void AddStocksFromFile()
        {
            try
            {
                stockPrices = FileManagement.stockText.Split(',');

                foreach (var price in stockPrices)
                {
                    stocks.Add(AddStock(stocks.Count + 1, Convert.ToDouble(price)));
                }

            }
            catch
            {
                if (stockPrices.Length == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Dataset error! Are you sure the stocks are comma separated?");
                    Console.ResetColor();
                    Console.WriteLine(".");
                    Console.WriteLine(".");
                    Console.WriteLine(".");
                    Console.WriteLine("Returning to path selection");
                    Console.WriteLine(".");
                    Console.WriteLine(".");
                    Console.WriteLine(".");

                    FileManagement.RequestPath();
                    AddStocksFromFile();
                }

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

        //TODO: Print results according to the requirement
        public void BuyOneSellOnePrint()
        {
            BuyStockSellStockProfit BSSS = investmentStrategies.BuyOneSellOne();
            if (BSSS.buyStock != null && BSSS.sellStock != null)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Printing results for the main strategy");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Stock Purchase Day: {BSSS.buyStock.Day} || Stock Purchase Price: {BSSS.buyStock.Price}");
                Console.WriteLine($"Stock Sell Day: {BSSS.sellStock.Day} || Stock Sell Price: {BSSS.sellStock.Price}");
                Console.WriteLine($"Profit: {BSSS.Profit()}");
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
            if (BSSS.buyStock != null && BSSS.sellStock != null)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Printing results for the alternative strategy");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Stock Purchase Day: {BSSS.buyStock.Day} || Stock Purchase Price: {BSSS.buyStock.Price}");
                Console.WriteLine($"Stock Sell Day: {BSSS.sellStock.Day} || Stock Sell Price: {BSSS.sellStock.Price}");
                Console.WriteLine($"Profit: {BSSS.Profit()}");
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





