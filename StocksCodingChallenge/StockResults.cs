using System.Collections.Generic;
using System.Linq;

namespace StocksCodingChallenge
{
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


}





