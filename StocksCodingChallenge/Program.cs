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
            db.PrintAllStocks();

            StockResults stockResults = new StockResults(db);
            Console.WriteLine($"The lower price is: {stockResults.minPrice()}");
            Console.WriteLine($"The highest price is: {stockResults.maxPrice()}");
            Console.WriteLine($"Day with lower price: {stockResults.StockWithLowerPrice().Day}, Price on that day: {stockResults.StockWithLowerPrice().Price}");
            Console.WriteLine($"Day with higher price: {stockResults.StockWithHigherPrice().Day}, Price on that day: {stockResults.StockWithHigherPrice().Price}");

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

            foreach(var price in stockPrices)
            {
                stocks.Add(AddStock(stocks.Count + 1, Convert.ToDouble(price)));
            }
        }

        public Stock AddStock(int day, double price)
        {
            return new Stock(day, price);
        }

        public void PrintAllStocks()
        {
            foreach (Stock stock in stocks)
            {
                Console.WriteLine($"Stock Day: {stock.Day} - Stock Price: {stock.Price}");
            }
        }
    }

    public class StockResults
    {
        public double minPrice() => db.stocks.Select(x=>x.Price).Min<double>();
        public double maxPrice() => db.stocks.Select(x=>x.Price).Max<double>();
        
        public Stock StockWithHigherPrice()
        {
            return db.stocks.Where(x => x.Price == maxPrice()).First();
        }
        public Stock StockWithLowerPrice()
        {
            return db.stocks.Where(x => x.Price == minPrice()).First();
        }

        public static Database db;

        public StockResults(Database database)
        {
            db = database;
        }


        
    }



    //TOOO: Investment Strategy

}
