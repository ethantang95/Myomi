using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myomi.Data
{
    internal class MyomiGestureProfile
    {
        IEnumerable<MyoDataProfile> Segments { get; set; }
        public MyomiProfileOptions Options { get; set; }

        public MyomiGestureProfile(MyomiProfileOptions options, IEnumerable<MyoDataProfile> segments) 
        {
            this.Segments = segments;
            this.Options = options;
        }
    }

    internal class MyomiProfileOptions 
    {
        public bool PoseEnabled { get; set; }
        public bool AccelEnabled { get; set; }
        public bool GyroEnabled { get; set; }
        public bool OrienEnabled { get; set; }
        public bool AccelNormOnly { get; set; }
        public bool GyroNormOnly { get; set; }
        //half mode would be that instead of divding it up into 4 zones, we divide it up into 2
        //one side will be 0, the other will be just 1
        public bool OrienHalfMode { get; set; }
        public bool IgnoreFrameCounts { get; set; }

        public MyomiProfileOptions() 
        {
            EnableAll();
        }

        public void EnableAll() 
        {
            this.PoseEnabled = true;
            this.AccelEnabled = true;
            this.GyroEnabled = true;
            this.OrienEnabled = true;
            this.AccelNormOnly = true;
            this.GyroNormOnly = true;
            this.OrienHalfMode = true;
            this.IgnoreFrameCounts = true;
        }

        public void DisableAll() 
        {
            //shouldn't really be called and left there... at least 1 setting should be enabled or else it's a really wierd
            //profile
            this.PoseEnabled = false;
            this.AccelEnabled = false;
            this.GyroEnabled = false;
            this.OrienEnabled = false;
            this.AccelNormOnly = false;
            this.GyroNormOnly = false;
            this.OrienHalfMode = false;
            this.IgnoreFrameCounts = false;
        }
    }
}
