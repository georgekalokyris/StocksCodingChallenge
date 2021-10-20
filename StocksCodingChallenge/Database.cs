using System;
using System.Collections.Generic;

namespace StocksCodingChallenge
{
    public class Database
    {

        public List<Stock> stocks = new List<Stock>();

        public void AddStocksFromFile()
        {
            string[] stockPrices = null;
            try
            {
                stockPrices = FileManagement.stockText.Split(',');
                AddStocksFromArray(stockPrices);
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
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Returning to path selection");
                    Console.ResetColor();
                    Console.WriteLine(".");
                    Console.WriteLine(".");
                    Console.WriteLine(".");

                    FileManagement.RequestPath();
                    AddStocksFromFile();
                }
            }
        }
        public bool AddStocksFromArray(string[] stockPrices)
        {
            if (stockPrices.Length == 0) return false;

            foreach (var price in stockPrices)
            {
                stocks.Add(AddStock(stocks.Count + 1, Convert.ToDouble(price)));
            }

            return true;
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


}





