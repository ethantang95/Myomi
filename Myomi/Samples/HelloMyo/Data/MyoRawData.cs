using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyoNet.Myo;
using Myomi.Instances;

//here we have the raw data that we will be obtained from the myo, given back in a form like object like this
//this then can be processed to create adjusted value, or parsing it to a MyoData class
namespace Myomi.Data
{
    internal interface IRawData 
    { }

    internal class PoseRawData : IRawData
    {
        public Pose Pose { get; set; }

        public PoseRawData(Pose fromMyo) 
        {
            this.Pose = fromMyo;
        }

    }
    //data obtained from the acceleronmeter
    internal class AccelerationRawData : IRawData
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Normal { get; set; }

        public AccelerationRawData(Vector3 fromMyo) 
        {
            this.X = fromMyo.X;
            this.Y = fromMyo.Y;
            this.Z = fromMyo.Z;
            double[] allVectors = { this.X, this.Y, this.Z };
            this.Normal = CommonOperations.GetNormal(allVectors);
        }
    }
    //data obtained from the 
    internal class GyroscopeRawData : IRawData
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Normal { get; set; }

        public GyroscopeRawData(Vector3 fromMyo) 
        {
            this.X = fromMyo.X;
            this.Y = fromMyo.Y;
            this.Z = fromMyo.Z;
            double[] allVectors = { this.X, this.Y, this.Z };
            this.Normal = CommonOperations.GetNormal(allVectors);
        }
    }

    internal class OrientationRawData : IRawData
    {
        //pitch is the rotation about the x axis
        public double Pitch { get; set; }
        //roll is the rotation about the y axis
        public double Roll { get; set; }
        //azimuth is the rotation about the z axis
        public double Azimuth { get; set; }

        public OrientationRawData(Quaternion fromMyo) 
        {
            this.Pitch = Quaternion.Pitch(fromMyo);
            this.Roll = Quaternion.Roll(fromMyo);
            this.Azimuth = Quaternion.Yaw(fromMyo);
        }
    }
    //this class shall be used for any raw data obtained from the myo, this is so that the raw data can be differentiated from other types
    internal class MyoRawData
    {
        public PoseRawData Pose { get; set; }
        public AccelerationRawData Accel { get; set; }
        public GyroscopeRawData Gyro { get; set; }
        public OrientationRawData Orien { get; set; }

        public MyoRawData(MyoState myoState) 
        {
            this.Pose = new PoseRawData(myoState.Pose);
            this.Accel = new AccelerationRawData(myoState.Accel);
            this.Gyro = new GyroscopeRawData(myoState.Gyro);
            this.Orien = new OrientationRawData(myoState.Orien);
        }
    }
}
