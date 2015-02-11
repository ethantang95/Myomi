using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

//any general methods or tasks that have to deal with files will be here, this includes restoring, getting, and writing to files
namespace Myomi.FromFiles
{
    internal static class FileManager
    {
        public static Dictionary<string, string> GetValueFromFile(string fileName)
        {
            var toReturn = new Dictionary<string, string>();
            try
            {
                using (var reader = new StreamReader(fileName))
                {
                    string line = reader.ReadLine();
                    var configLine = line.Split(':');
                    toReturn.Add(configLine[0], configLine[1]);
                }
            }
            catch (Exception e)
            {
                if (e is IOException || e is IndexOutOfRangeException)
                {
                    Console.WriteLine("The {0} file is corrupt, attempting to reset the file.", fileName);
                }
                else
                {
                    throw e;
                }
            }
            return toReturn;
        }
        public static bool WriteToFile(Dictionary<string, string> toWrite, string fileName)
        {
            try
            {
                using (var writer = new StreamWriter(fileName)) 
                {
                    foreach (var entry in toWrite) 
                    {
                        writer.WriteLine("{0}:{1}", entry.Key, entry.Value);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong while writing {0} file", fileName);
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        public static bool RecoverPointsFile() 
        {
            try
            {
                using (var writer = new StreamWriter("Points.config"))
                {
                    writer.WriteLine("Match:0");
                    writer.WriteLine("SlightMatch:6");
                    writer.WriteLine("NoMatch:21");
                    writer.WriteLine("NotRest:12");
                    writer.WriteLine("NotAnalyzed:0");
                    writer.WriteLine("NoMatchPose:50");
                    writer.WriteLine("NotAnalyzedPose:0");
                    writer.WriteLine("NoMatchOrientation:6");
                    writer.WriteLine("NotAnalyzedOrientation:0");
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong while trying to recover the Points.config file");
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
    }
}
