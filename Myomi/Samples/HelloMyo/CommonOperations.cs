using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

//this class is used for any options and stuff that are well... common...
namespace Myomi
{
    internal static class CommonOperations
    {
        public static double GetNormal(double[] vectorLengths)
        {
            double toReturn = 0;
            for (int i = 0; i < vectorLengths.Length; i++) 
            {
                toReturn += vectorLengths[i] * vectorLengths[i];
            }
            toReturn = Math.Sqrt(toReturn);
            return toReturn;
        }

        public static void Sleep(int time) 
        {
            Thread.Sleep(time);
        }

        public static bool GetYesOrNo() 
        {
            string option = Console.ReadLine();
            while (option != "yes" && option != "no")
            {
                Console.WriteLine("Please enter a valid choice (Type 'yes' or 'no')");
                option = Console.ReadLine();
            }
            if (option == "yes")
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public static int GetHalfModeValue(int value)
        {
            if (value > 3 || value < 0)
            {
                throw new ArgumentException();
            }
            else if (value == 3 || value == 2)
            {
                return 0;
            }
            else
            {
                return 2;
            }
        }

        public static int GetOptionValue(string optionString, int start, int end)
        {
            int option;
            if (!Int32.TryParse(optionString, out option))
            {
                return -1;
            }
            //0 is our default exit option
            if ((option > end || option < start) && option != 0)
            {
                return -1;
            }
            else
            {
                return option;
            }
        }
    }
}
