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
        void SetToNone();
    }

    internal class PoseProfileData : PoseData, IProfileData
    {
        public void SetToNone()
        {
            this.Pose = Pose.Unknown;
        }

        public string ToFileString() 
        {
            return this.Pose.ToString();
        }
    }
    //data obtained from the acceleronmeter
    internal class AcceleronmeterProfileData : AcceleronmeterData, IProfileData
    {
        public void SetToNone() 
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
            this.Normal = 0;
        }

        public string ToFileString() 
        {
            return String.Format("{0}:{1}:{2}:{3}", this.X, this.Y, this.Z, this.Normal);
        }
    }
    //data obtained from the 
    internal class GyroscopeProfileData : GyroscopeData, IProfileData
    {
        public void SetToNone() 
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
            this.Normal = 0;
        }

        public string ToFileString()
        {
            return String.Format("{0}:{1}:{2}:{3}", this.X, this.Y, this.Z, this.Normal);
        }
    }

    internal class OrientationProfileData : OrientationData, IProfileData
    {
        public void SetToNone() 
        {
            this.Pitch = 0;
            this.Roll = 0;
            this.Azimuth = 0;
        }

        public string ToFileString()
        {
            return String.Format("{0}:{1}:{2}", this.Pitch, this.Roll, this.Azimuth);
        }
    }

    internal class MyoDataProfile : ICloneable, IComparable
    {
        public PoseProfileData Pose { get; set; }
        public AcceleronmeterProfileData Accel { get; set; }
        public GyroscopeProfileData Gyro { get; set; }
        public OrientationProfileData Orien { get; set; }
        public int Frames { get; set; }

        public MyoDataProfile() 
        {
            this.Pose = new PoseProfileData();
            this.Accel = new AcceleronmeterProfileData();
            this.Gyro = new GyroscopeProfileData();
            this.Orien = new OrientationProfileData();
            this.Frames = 0;
        }

        public MyoDataProfile(string toParse) 
        {
            var values = toParse.Split(':');
            //the format should be pose:acceldata:gyrodat:oriendata:frames
            this.Pose = new PoseProfileData {Pose = (Pose) Enum.Parse(typeof (Pose), values[0])};

            this.Accel = new AcceleronmeterProfileData
            {
                X = Int32.Parse(values[1]),
                Y = Int32.Parse(values[2]),
                Z = Int32.Parse(values[3]),
                Normal = Int32.Parse(values[4])
            };

            this.Gyro = new GyroscopeProfileData
            {
                X = Int32.Parse(values[5]),
                Y = Int32.Parse(values[6]),
                Z = Int32.Parse(values[7]),
                Normal = Int32.Parse(values[8])
            };

            this.Orien = new OrientationProfileData
            {
                Pitch = Int32.Parse(values[9]),
                Roll = Int32.Parse(values[9]),
                Azimuth = Int32.Parse(values[9])
            };
        }
        public static MyoDataProfile ConvertToProfile(MyoData toConvert) 
        {
            var toReturn = new MyoDataProfile
            {
                Pose = (PoseProfileData) toConvert.Pose.Clone(),
                Accel = (AcceleronmeterProfileData) toConvert.Accel.Clone(),
                Gyro = (GyroscopeProfileData) toConvert.Gyro.Clone(),
                Orien = (OrientationProfileData) toConvert.Orien.Clone(),
                Frames = 1
            };
            //if we are converting to profile, it should always be the beginning of a frame set
            return toReturn;
        }

        public object Clone()
        {
            var clone = new MyoDataProfile
            {
                Pose = (PoseProfileData) this.Pose.Clone(),
                Accel = (AcceleronmeterProfileData) this.Accel.Clone(),
                Gyro = (GyroscopeProfileData) this.Gyro.Clone(),
                Orien = (OrientationProfileData) this.Orien.Clone(),
                Frames = 0
            };
            return clone;
        }

        public int CompareTo(object obj)
        {
            //I am just going to return 0 (not equal) and 1 (equal)
            var toCompare = (MyoDataProfile)obj;
            bool equal = true;
            equal &= (this.Pose.CompareTo(toCompare.Pose) == 1);
            equal &= (this.Accel.CompareTo(toCompare.Accel) == 1);
            equal &= (this.Gyro.CompareTo(toCompare.Gyro) == 1);
            equal &= (this.Orien.CompareTo(toCompare.Orien) == 1);
            return (equal ? 1 : 0);
        }

        public string ToFileString()
        {
            return String.Format("{0}:{1}:{2}:{3}:{4}", this.Pose.ToFileString(), this.Accel.ToFileString(), this.Gyro.ToFileString(), this.Orien.ToFileString().Length, this.Frames);
        }
    }
}
