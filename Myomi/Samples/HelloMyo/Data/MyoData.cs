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

    internal class PoseData : IData, ICloneable, IComparable
    {
        public Pose Pose { get; set; }

        public object Clone()
        {
            var clone = new PoseData();
            clone.Pose = this.Pose;
            return clone;
        }

        public int CompareTo(object obj)
        {
            var toCompare = (PoseData)obj;
            return this.Pose == toCompare.Pose ? 1 : 0;
        }
    }
    //data obtained from the acceleronmeter
    internal class AcceleronmeterData : IData, ICloneable, IComparable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Normal { get; set; }

        public object Clone()
        {
            var clone = new AcceleronmeterData
            {
                X = this.X,
                Y = this.Y,
                Z = this.Z,
                Normal = this.Normal
            };
            return clone;
        }

        public int CompareTo(object obj)
        {
            var toCompare = (AcceleronmeterData)obj;
            bool equal = true;
            equal &= (this.X == toCompare.X);
            equal &= (this.Y == toCompare.Y);
            equal &= (this.Z == toCompare.Z);
            //for normals, I will allow it to have an offset of 1
            equal &= (Math.Abs(this.Normal - toCompare.Normal) <= 1);
            
            return (equal? 1 : 0);
        }
    }
    //data obtained from the 
    internal class GyroscopeData : IData, ICloneable, IComparable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Normal { get; set; }

        public object Clone()
        {
            var clone = new GyroscopeData
            {
                X = this.X,
                Y = this.Y,
                Z = this.Z,
                Normal = this.Normal
            };
            return clone;
        }

        public int CompareTo(object obj)
        {
            var toCompare = (GyroscopeData)obj;
            bool equal = true;
            equal &= (this.X == toCompare.X);
            equal &= (this.Y == toCompare.Y);
            equal &= (this.Z == toCompare.Z);
            //for normals, I will allow it to have an offset of 1
            equal &= (Math.Abs(this.Normal - toCompare.Normal) <= 1);

            return (equal ? 1 : 0);
        }
    }

    internal class OrientationData : IData, ICloneable, IComparable
    {
        //pitch is the rotation about the x axis
        public int Pitch { get; set; }
        //roll is the rotation about the y axis
        public int Roll { get; set; }
        //azimuth is the rotation about the z axis
        public int Azimuth { get; set; }

        public object Clone()
        {
            var clone = new OrientationData
            {
                Pitch = this.Pitch,
                Roll = this.Roll,
                Azimuth = this.Azimuth
            };
            return clone;
        }

        public int CompareTo(object obj)
        {
            var toCompare = (OrientationData)obj;
            bool equal = true;
            equal &= (this.Pitch == toCompare.Pitch);
            equal &= (this.Roll == toCompare.Roll);
            equal &= (this.Azimuth == toCompare.Azimuth);

            return (equal ? 1 : 0);
        }
    }

    //in here, we are using int to measure the relative value
    //0 means rest (or neglectable), 1 means slow, 2 means medium, 3 means fast
    //for orientation, it is with respect to the cartesian quadrants with the vector facing towards you
    //1 is 0 to 90, 2 is 90 to 180, 3 is 180 to 270, 4 is 270 to 360 or 0
    internal class MyoData: ICloneable, IComparable
    {
        public PoseData Pose { get; set; }
        public AcceleronmeterData Accel { get; set; }
        public GyroscopeData Gyro { get; set; }
        public OrientationData Orien { get; set; }

        public object Clone()
        {
            var clone = new MyoData
            {
                Pose = (PoseData) this.Pose.Clone(),
                Accel = (AcceleronmeterData) this.Accel.Clone(),
                Gyro = (GyroscopeData) this.Gyro.Clone(),
                Orien = (OrientationData) this.Orien.Clone()
            };
            return clone;
        }

        public int CompareTo(object obj)
        {
            //I am just going to return 0 (not equal) and 1 (equal)
            var toCompare = (MyoData)obj;
            bool equal = true;
            equal &= (this.Pose.CompareTo(toCompare.Pose) == 1);
            equal &= (this.Accel.CompareTo(toCompare.Accel) == 1);
            equal &= (this.Gyro.CompareTo(toCompare.Gyro) == 1);
            equal &= (this.Orien.CompareTo(toCompare.Orien) == 1);
            return (equal ? 1 : 0);
        }
    }
}
