using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyoNet.Myo;

namespace Myomi.Data
{
    public interface IData 
    { }

    public class PoseData : IData 
    {
        
        public Pose Pose { get; set; }
    }
    //data obtained from the acceleronmeter
    public class AccelerationData : IData
    {
        int X { get; set; }
        int Y { get; set; }
        int Z { get; set; }
        int Scalar { get; set; }
    }
    //data obtained from the 
    public class GyroscopeData : IData
    {
        int X { get; set; }
        int Y { get; set; }
        int Z { get; set; }
        int Scalar { get; set; }
    }

    public class OrientationData : IData
    {
        //pitch is the rotation about the x axis
        int Pitch { get; set; }
        //roll is the rotation about the y axis
        int Roll { get; set; }
        //azimuth is the rotation about the z axis
        int Azimuth { get; set; }
    }

    //in here, we are using int to measure the relative value
    //0 means rest (or neglectable), 1 means slow, 2 means medium, 3 means fast
    //for orientation, it is with respect to the cartesian quadrants with the vector facing towards you
    //1 is 0 to 90, 2 is 90 to 180, 3 is 180 to 270, 4 is 270 to 360 or 0
    public class MyoData
    {
        public PoseData Pose { get; set; }
        public AccelerationData Accel { get; set; }
        public GyroscopeData Gyro { get; set; }
        public OrientationData Orien { get; set; }
    }
}
