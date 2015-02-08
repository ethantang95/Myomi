using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyoNet.Myo;

//This is the data we have obtained from the profiles, which we will be comparing with what we get from the Myo with
namespace Myomi.Data
{
    internal interface IProfileData 
    {
        bool Enabled { get; set; }
    }

    internal class PoseProfileData : IProfileData
    {
        public Pose Pose { get; set; }
        public bool Enabled { get; set; }
    }
    //data obtained from the acceleronmeter
    internal class AccelerationProfileData : IProfileData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Normal { get; set; }
        public bool NormalOnly { get; set; }
        public bool Enabled { get; set; }
    }
    //data obtained from the 
    internal class GyroscopeProfileData : IProfileData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Normal { get; set; }
        public bool NormalOnly { get; set; }
        public bool Enabled { get; set; }
    }

    internal class OrientationProfileData : IProfileData
    {
        //pitch is the rotation about the x axis
        public int Pitch { get; set; }
        //roll is the rotation about the y axis
        public int Roll { get; set; }
        //azimuth is the rotation about the z axis
        public int Azimuth { get; set; }
        //half mode would be that instead of divding it up into 4 zones, we divide it up into 2
        //one side will be 0, the other will be just 1
        public bool HalfMode { get; set; }
        public bool Enabled { get; set; }
    }

    internal class MyoDataProfile
    {
        public PoseProfileData Pose;
        public AccelerationProfileData Accel;
        public GyroscopeProfileData Gyro;
        public OrientationProfileData Orien;
    }

}
