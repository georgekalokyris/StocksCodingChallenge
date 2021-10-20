using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace StocksCodingChallenge
{
    public class Tests
    {
        [Test]
        public void TestDataset1()
        {
            double[] array = new double[] { 18.93, 20.25, 17.05, 16.59, 21.09, 16.22, 21.43, 27.13, 18.62, 21.31, 23.96, 25.52, 19.64, 23.49, 15.28, 22.77, 23.1, 26.58, 27.03, 23.75, 27.39, 15.93, 17.83, 18.82, 21.56, 25.33, 25, 19.33, 22.08, 24.03 };

            TestTemplate(array, 15.28, 15, 27.39, 21);
        }
        
        [Test]
        public void TestDataset2()
        {
            double[] array2 = new double[] { 22.74, 22.27, 20.61, 26.15, 21.68, 21.51, 19.66, 24.11, 20.63, 20.96, 26.56, 26.67, 26.02, 27.20, 19.13, 16.57, 26.71, 25.91, 17.51, 15.79, 26.19, 18.57, 19.03, 19.02, 19.97, 19.04, 21.06, 25.94, 17.03, 15.61 };

            TestTemplate(array2, 15.79, 20, 26.19, 21);
        }

        [Test]
        public void TestAscending()
        {
            double[] asc = new double[] { 0.1, 0.2, 0.3, 0.4, 0.6, 1.1 };

            TestTemplate(asc, asc[0], 1, asc[asc.Length - 1], asc.Length);
        }

        [Test]
        public void TestEmpty()
        {
            double[] empty = new double[0];

            Database db = new Database();
            Assert.IsFalse(db.AddStocksFromArray(empty.Select(x => x.ToString()).ToArray()));
        }

        [Test]
        public void TestOne()
        {
            double[] one = new double[] { 5.1 };

            TestTemplate(one, one[0], 1, one[0], 1);
        }

        [Test]
        public void TestDescending()
        {
            double[] desc = new double[] { 1.1 , 0.8 , 0.6, 0.4, 0.3, 0.1 };

            TestTemplate(desc, desc[0], 1, desc[0], 1);
        }


        public void TestTemplate(double[] arr, double expBuyPrice, int expBuyDay, double expSellPrice, int expSellDay)
        {
            Database db = new Database();
            db.AddStocksFromArray(arr.Select(x => x.ToString()).ToArray());
            StockResults stockResults = new StockResults(db);
            InvestmentStrategies investmentStr = new InvestmentStrategies(stockResults);
            Print print = new Print(stockResults, investmentStr);

            BuyStockSellStockProfit BSSS = new BuyStockSellStockProfit(
                new Stock(expBuyDay, expBuyPrice),
                new Stock(expSellDay, expSellPrice));

            CheckEquality(BSSS, investmentStr);
        }

        public void CheckEquality(BuyStockSellStockProfit BSSS, InvestmentStrategies inv)
        {
            BuyStockSellStockProfit BSSSresult = inv.BuyOneSellOne();

            Assert.AreEqual(BSSSresult.buyStock.Day, BSSS.buyStock.Day);
            Assert.AreEqual(BSSSresult.sellStock.Day, BSSS.sellStock.Day);
            Assert.AreEqual(BSSSresult.buyStock.Price, BSSS.buyStock.Price);
            Assert.AreEqual(BSSSresult.sellStock.Price, BSSS.sellStock.Price);
        }
    }
}