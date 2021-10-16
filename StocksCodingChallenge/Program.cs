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

            Database db = new Database();
            db.AddStocksFromFile();

            Print print = new Print(db);

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
        public static string filePath = @"C:\Users\kalok\Downloads\Computershare - Coding Challenge\ChallengeSampleDataSet1.txt";

        //TODO:
        public static void RequestPath()
        {
            Console.WriteLine("Not yet Implemented");
        }
    }

    public class Database
    {

        public List<Stock> stocks = new List<Stock>();
        public void AddStocksFromFile()
        {
            string stockText = File.ReadAllText(FileManagement.filePath);

            string[] stockPrices = stockText.Split(',');

            foreach (var price in stockPrices)
            {
                stocks.Add(AddStock(stocks.Count + 1, Convert.ToDouble(price)));
            }
        }

        public Stock AddStock(int day, double price)
        {
            return new Stock(day, price);
        }

        
    }

    public class StockResults
    {
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



        public static Database db;

        public StockResults(Database database)
        {
            db = database;
        }


    }

    public class Print
    {

        public void PrintAllStocks()
        {
            Console.WriteLine("Printing all Stocks as read from database");
            foreach (Stock stock in data.stocks)
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


        public static Database data;

        StockResults stockResults = new StockResults(data);

        public Print(Database db)
        {
            data = db;
        }

    }

}



    //TOOO: Investment Strategy

