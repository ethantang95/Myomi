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
    }
}
