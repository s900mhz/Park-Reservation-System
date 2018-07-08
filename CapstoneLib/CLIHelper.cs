using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CapstoneLib
{
    public class CLIHelper
    {
        public static int GetInteger(string message)
        {
            string userInput = "";
            int intValue = 0;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }

                Console.Write(message + " ");
                userInput = Console.ReadKey().KeyChar.ToString();
                numberOfAttempts++;
            }
            while (!int.TryParse(userInput, out intValue));

            return intValue;

        }
        public static int GetInteger()
        {
            string userInput = "";
            int intValue = 0;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }


                userInput = Console.ReadKey().KeyChar.ToString();
                numberOfAttempts++;
            }
            while (!int.TryParse(userInput, out intValue));

            return intValue;

        }
        
        public static int GetInteger(int menuoptions)
        {
            string userInput = "";
            int intValue = 0;
            int numberOfAttempts = 0;
            bool isInt;
            
            do
            {
                if (numberOfAttempts > 0)
                {
                    
                    Console.WriteLine("<---Invalid input format. Please try again");
                    Thread.Sleep(1000);
                    ClearLastLine();
                }


                userInput = Console.ReadKey().KeyChar.ToString();
                numberOfAttempts++;
                isInt = int.TryParse(userInput, out intValue);
                //isNotOption = (intValue >= menuoptions);
            }
            while (!isInt || !(intValue<=menuoptions));




            return intValue;

        }


        public static double GetDouble(string message)
        {
            string userInput = String.Empty;
            double doubleValue = 0.0;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("<----Invalid input format. Please try again");
                }

                Console.Write(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!double.TryParse(userInput, out doubleValue));

            return doubleValue;

        }

        public static bool GetBool(string message)
        {
            string userInput = String.Empty;
            bool boolValue = false;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }

                Console.Write(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!bool.TryParse(userInput, out boolValue));

            return boolValue;
        }

        public static string GetString(string message)
        {
            string userInput = String.Empty;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }

                Console.Write(message + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (String.IsNullOrEmpty(userInput));

            return userInput;
        }

        public static DateTime GetDate(string message)
        {
            if (!DateTime.TryParse(message, out DateTime dateTime))
            {
                Console.WriteLine("Invalid Date");

            }
            return dateTime;
        }
        public static void ClearLastLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }

}

