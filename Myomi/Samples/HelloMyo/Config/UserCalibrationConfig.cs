using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.TaskManager;
using MyoNet.Myo;

//the user configuration, currently we will support 1 profile but in the future, we will be able to support multiple profiles
namespace Myomi.Config
{
    internal class UserCalibrationConfig
    {
        static string _fileName = "UserCalibration.config";
        public double FastAccel { get; set; }
        public double SlowAccel { get; set; }
        public double FastGyro { get; set; }
        public double SlowGyro { get; set; }
        public Arm Arm { get; set; }

        public UserCalibrationConfig()
        {
            Initialize();
        }
        public void Initialize()
        {
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
                FastAccel = Double.Parse(configRead["FastAccel"]);
                SlowAccel = Double.Parse(configRead["SlowAccel"]);
                FastGyro = Double.Parse(configRead["FastGyro"]);
                SlowGyro = Double.Parse(configRead["SlowGyro"]);
                if (configRead["Arm"] == Arm.Left.ToString()) 
                {
                    Arm = Arm.Left;
                }
                else if (configRead["Arm"] == Arm.Right.ToString()) 
                {
                    Arm = Arm.Right;
                }
                else if (configRead["Arm"] == Arm.Unknown.ToString())
                {
                    Arm = Arm.Unknown;
                }
                else
                {
                    throw new FormatException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught at SetValue at UserCalibrationConfig");
                Console.WriteLine(e.ToString());
                if (e is KeyNotFoundException || e is FormatException) 
                {
                    Console.WriteLine("The {0} file is corrupt, the calibration must be redone", _fileName);
                    RecoverFile();
                }
            }
        }
        private void RecoverFile() 
        {
            //here, we start a user calibration manager which will recollect the user's calibrations
            //then we populate this field and write to file
            var userCalibrationManager = new UserCalibrationManager();
            userCalibrationManager.Run();
            var collected = userCalibrationManager.GetCollected();
            SetValue(collected);
            FileHelper.WriteToFile(collected, _fileName);
        }
    }
}
