using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyoNet.Myo;

//This is the data we have obtained from the profiles, which we will be comparing with what we get from the Myo with
namespace Myomi.Data
{
    internal interface IProfileData 
    { }

    internal class PoseProfileData : IProfileData
    {
        public Pose Pose { get; set; }
    }
    //data obtained from the acceleronmeter
    internal class AcceleronmeterProfileData : IProfileData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Normal { get; set; }
    }
    //data obtained from the 
    internal class GyroscopeProfileData : IProfileData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Normal { get; set; }
    }

    internal class OrientationProfileData : IProfileData
    {
        //pitch is the rotation about the x axis
        public int Pitch { get; set; }
        //roll is the rotation about the y axis
        public int Roll { get; set; }
        //azimuth is the rotation about the z axis
        public int Azimuth { get; set; }
    }

    internal class MyoDataProfile
    {
        public PoseProfileData Pose { get; set; }
        public AcceleronmeterProfileData Accel { get; set; }
        public GyroscopeProfileData Gyro { get; set; }
        public OrientationProfileData Orien { get; set; }
        public int Frames { get; set; }
    }

}
