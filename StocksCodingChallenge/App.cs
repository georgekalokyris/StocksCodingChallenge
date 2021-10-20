using System;

namespace StocksCodingChallenge
{
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


}





