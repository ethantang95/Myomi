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
            var pointsConfigRead = FileManager.GetValueFromFile("Points.config");
            setValue(pointsConfigRead);
        }

        private void setValue(Dictionary<string, string> pointsConfigRead)
        {
            try
            {
                Match = Int32.Parse(pointsConfigRead["Match"]);
                SlightMatch = Int32.Parse(pointsConfigRead["SlightMatch"]);
                NoMatch = Int32.Parse(pointsConfigRead["NoMatch"]);
                NotRest = Int32.Parse(pointsConfigRead["NotRest"]);
                NotAnalyzed = Int32.Parse(pointsConfigRead["NotAnalyzed"]);
                NoMatchPose = Int32.Parse(pointsConfigRead["NoMatchPose"]);
                NotAnalyzedPose = Int32.Parse(pointsConfigRead["NotAnalyzedPose"]);
                NoMatchOrientation = Int32.Parse(pointsConfigRead["NoMatchOrientation"]);
                NotAnalyzedOrientation = Int32.Parse(pointsConfigRead["NotAnalyzedOrientation"]);
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
