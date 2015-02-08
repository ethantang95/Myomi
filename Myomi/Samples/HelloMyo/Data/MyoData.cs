using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyoNet.Myo;

//this class is made for the data to analyze to see matchings. It has simplied the raw data into integer levels
namespace Myomi.Data
{
    internal interface IData 
    { }

    internal class PoseData : IData 
    {
        public Pose Pose { get; set; }
    }
    //data obtained from the acceleronmeter
    internal class AccelerationData : IData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Normal { get; set; }
    }
    //data obtained from the 
    internal class GyroscopeData : IData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Normal { get; set; }
    }

    internal class OrientationData : IData
    {
        //pitch is the rotation about the x axis
        public int Pitch { get; set; }
        //roll is the rotation about the y axis
        public int Roll { get; set; }
        //azimuth is the rotation about the z axis
        public int Azimuth { get; set; }
    }

    //in here, we are using int to measure the relative value
    //0 means rest (or neglectable), 1 means slow, 2 means medium, 3 means fast
    //for orientation, it is with respect to the cartesian quadrants with the vector facing towards you
    //1 is 0 to 90, 2 is 90 to 180, 3 is 180 to 270, 4 is 270 to 360 or 0
    internal class MyoData
    {
        public PoseData Pose { get; set; }
        public AccelerationData Accel { get; set; }
        public GyroscopeData Gyro { get; set; }
        public OrientationData Orien { get; set; }
    }
}
