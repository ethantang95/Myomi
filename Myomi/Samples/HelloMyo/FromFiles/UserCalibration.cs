using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyoNet.Myo;

namespace Myomi.FromFiles
{
    internal class UserCalibration
    {
        public double FastAccel { get; set; }
        public double SlowAccel { get; set; }
        public double FastGyro { get; set; }
        public double SlowGyro { get; set; }
        public Arm Arm { get; set; }

    }
}
