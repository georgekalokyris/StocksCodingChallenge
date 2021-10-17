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
            StockResults stockResults = new StockResults(db);
            InvestmentStrategies investments = new InvestmentStrategies(stockResults);
            Print print = new Print(stockResults);

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
            investments.BuyOneSellOne();

            Console.WriteLine("Please provide the path or press x for defualt");
            string newPath = Console.ReadLine();
            Console.WriteLine(newPath);

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

        //TODO: CRUD on Stock

        
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


        StockResults stockResults;

        public Print(StockResults stockResults)
        {
            this.stockResults = stockResults;
        }

    }

    struct BuyStockSellStock
    {
        public Stock buyStock;
        public Stock sellStock;
    }

    class InvestmentStrategies
    {
        public double Profit;
        //Default requirement for buying a single stock and then selling it at the highest possible price
        
        public void BuyOneSellOne()
        {
            //Find the Stock with the Lowest Price
            Stock buyStock = stockResults.StockWithLowerPrice();
            //Find the Stock with the Higher Price
            Stock sellStock = stockResults.StockWithHigherPrice();
           
            //while (!(buyStock.Day < sellStock.Day))
            //{
            //    int buyIndex = 1;
            //    int sellIndex = 1;

            //    if(buyStock.Day < stockResults.db.stocks.Count)
            //    {
            //        buyStock = stockResults.StocksByPriceAsc()[buyIndex++];
            //        possibleProfitA = sellStock.Price - buyStock.Price;
            //    }
            //    else
            //    {
            //        buyStock = stockResults.StockWithLowerPrice();
            //        sellStock = stockResults.StocksByPriceDesc()[sellIndex++];
            //        possibleProfitB = sellStock.Price - buyStock.Price;

            //    }
            //    //sellStock = stockResults.StocksByPriceDesc()[sellIndex++];
            //}

            double maxDiff = 0;
            int buyDay = 0;
            int sellDay = 0;
            for (int i = 0; i < stockResults.db.stocks.Count(); i++) //Buy
            {
                
                for (int j = i+1; j < stockResults.db.stocks.Count(); j++) //Sell
                {
                    double diff = stockResults.db.stocks[j].Price - stockResults.db.stocks[i].Price;
                    

                    //if (maxDiff < diff) maxDiff = diff; buyDay = i; sellDay = j;

                    if (maxDiff < diff)
                    {
                        maxDiff = diff;
                        buyDay = i+1;
                        sellDay = j+1;
                    }

                    Console.WriteLine($"Profit: Buy Day: {stockResults.db.stocks[i].Day} i:[ {i}] Sell Day: {stockResults.db.stocks[j].Day} {diff}");

                }
            }
            BuyStockSellStock BSSS = new BuyStockSellStock();
            BSSS.buyStock = stockResults.db.stocks[buyDay - 1];
            BSSS.sellStock = stockResults.db.stocks[sellDay - 1];


            Console.WriteLine($"Buy Day: {BSSS.buyStock.Day} Price buy: {BSSS.buyStock.Price}");
            Console.WriteLine($"Sell Day: {BSSS.sellStock.Day} Price sell: {BSSS.sellStock.Price}");
            //Console.WriteLine($"A  {buyDay}");
            //Console.WriteLine($"B  {sellDay}");
            //Profit = sellStock.Price - buyStock.Price;

            //Console.WriteLine($"Your total earnings are: Sell Price: {sellStock.Price} - Buy Price: {buyStock.Price} = Profit {Profit}");
        }

        StockResults stockResults;
        public InvestmentStrategies(StockResults stockResults)
        {
            this.stockResults = stockResults;
        }
    }

}



    

