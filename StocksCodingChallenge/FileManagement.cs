using System;
using System.IO;

namespace StocksCodingChallenge
{
    public static class FileManagement
    {

        public static string filePath = "";
        public static string defaultPath = @"C:\ChallengeSampleDataSet1.txt";
        public static string stockText = "";

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


}





