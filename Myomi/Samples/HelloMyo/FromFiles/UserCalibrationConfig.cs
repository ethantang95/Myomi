using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyoNet.Myo;

//the user configuration, currently we will support 1 profile but in the future, we will be able to support multiple profiles
namespace Myomi.FromFiles
{
    internal class UserCalibrationConfig
    {
        static string _fileName = "UserCalibration.config";
        public double FastAccel { get; set; }
        public double SlowAccel { get; set; }
        public double FastGyro { get; set; }
        public double SlowGyro { get; set; }
        public Arm Arm { get; set; }

    }
}
