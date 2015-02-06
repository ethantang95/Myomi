using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Myomi.FromFiles
{
    public class PointsConfig
    {
        public int Match { get; private set; }
        public int SlightMatch { get; private set; }
        public int NoMatch { get; private set; }
        public int NotRest { get; private set; }
        public int NotAnalyzed { get; private set; }
        public int NoMatchPose { get; private set; }
        public int NotAnalyzedPose { get; private set; }
        public int NoMatchOrientation { get; private set; }
        public int NotAnalyzedOrientation { get; private set; }

        public PointsConfig()
        {
            Initialize();
        }

        public void Initialize() 
        { 
            //initalizes the values of such by reading the config file
            var pointsConfigRead = new Dictionary<string, int>();
            try
            {
                using (var sr = new StreamReader("Points.config"))
                {
                    string line = sr.ReadLine();
                    var configLine = line.Split(':');
                    pointsConfigRead.Add(configLine[0], Int32.Parse(configLine[1]));
                }
            }
            catch
            {
                Console.WriteLine("The Points.config file is corrupt, attempting to reset the file.");
            }
        }

        private void setValue(Dictionary<string, int> pointsConfigRead)
        {
            try
            {
                Match = pointsConfigRead["Match"];
                SlightMatch = pointsConfigRead["SlightMatch"];
                NoMatch = pointsConfigRead["NoMatch"];
                NotRest = pointsConfigRead["NotRest"];
                NotAnalyzed = pointsConfigRead["NotAnalyzed"];
                NoMatchPose = pointsConfigRead["NoMatchPose"];
                NotAnalyzedPose = pointsConfigRead["NotAnalyzedPose"];
                NoMatchOrientation = pointsConfigRead["NoMatchOrientation"];
                NotAnalyzedOrientation = pointsConfigRead["NotAnalyzedOrientation"];
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine("The Points.config file is corrupt, attempting to reset the file.");
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception e) 
            {
                Console.WriteLine("Something went horribly wrong... how?");
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
