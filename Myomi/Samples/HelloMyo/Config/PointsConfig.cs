using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Myomi.Config
{
    // the file for setting the points of program which will be used to calculate matching gestures
    // if any new point profile is added, remember include them into SetValue and RecoverFile
    public class PointsConfig
    {
        static string _fileName = "Points.config";
        public int Match { get; private set; }
        public int SlightMatch { get; private set; }
        public int NoMatch { get; private set; }
        public int NotRest { get; private set; }
        public int NotAnalyzed { get; private set; }
        public int NoMatchPose { get; private set; }
        public int NotAnalyzedPose { get; private set; }
        public int NotRestPose { get; private set; }
        public int NoMatchOrientation { get; private set; }
        public int NotAnalyzedOrientation { get; private set; }

        public PointsConfig()
        {
            Initialize();
        }

        public void Initialize() 
        { 
            //initalizes the values of such by reading the config file
            var configRead = FileHelper.GetValueFromFile(_fileName);
            if (configRead == null)
            {
                RecoverFile();
            }
            SetValue(configRead);
        }

        private void SetValue(Dictionary<string, string> configRead)
        {
            try
            {
                Match = Int32.Parse(configRead["Match"]);
                SlightMatch = Int32.Parse(configRead["SlightMatch"]);
                NoMatch = Int32.Parse(configRead["NoMatch"]);
                NotRest = Int32.Parse(configRead["NotRest"]);
                NotAnalyzed = Int32.Parse(configRead["NotAnalyzed"]);
                NoMatchPose = Int32.Parse(configRead["NoMatchPose"]);
                NotAnalyzedPose = Int32.Parse(configRead["NotAnalyzedPose"]);
                NotRestPose = Int32.Parse(configRead["NotRestPose"]);
                NoMatchOrientation = Int32.Parse(configRead["NoMatchOrientation"]);
                NotAnalyzedOrientation = Int32.Parse(configRead["NotAnalyzedOrientation"]);
            }
            catch (Exception e) 
            {
                Console.WriteLine("Exception caught at SetValue at PointsConfig");
                Console.WriteLine(e.ToString());
                if (e is KeyNotFoundException)
                {
                    Console.WriteLine("The {0} file is corrupt, attempting to reset the file.", _fileName);
                    RecoverFile();

                }
                else
                {
                    Console.WriteLine("Something went horribly wrong... how?");
                }
            }
        }

        private void RecoverFile() 
        {
            var config = new Dictionary<string, string>();
            config.Add("Match", "0");
            config.Add("SlightMatch", "6");
            config.Add("NoMatch", "21");
            config.Add("NotRest", "12");
            config.Add("NotAnalyzed", "0");
            config.Add("NoMatchPose", "50");
            config.Add("NotAnalyzedPose", "0");
            config.Add("NotRestPose", "15");
            config.Add("NoMatchOrientation", "6");
            config.Add("NotAnalyzedOrientation", "0");

            if (!FileHelper.WriteToFile(config, _fileName)) 
            {
                Console.WriteLine("Failure to recover {0}, now exiting program", _fileName);
                System.Environment.Exit(-1);
            }
            else
            {
                //here, we have a recover success
                Console.WriteLine("Successful recovery of {0}, reinitalizing", _fileName);
                this.Initialize();
            }
        }
    }
}
