using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyoNet.Myo;

namespace Myomi.Instances
{
    class MyomiMyo
    {

        IMyo myo;

        public void Initialize()
        {
            while (myo == null)
            {
                myo = Context.ProgramContext.Hub.GetMyo();
            }

            myo.AccelerometerDataAcquired += OnAccelerometerData;
            myo.GyroscopeDataAquired += OnGyroscopeData;
            myo.OrientationDataAcquired += OnOrientationData;
            myo.PoseChanged += OnPoseChanged;
            myo.RecognizedArm += OnRecognizedArm;
            myo.LostArm += OnLostArm;
        }

        static void OnAccelerometerData(object sender, AccelerometerDataEventArgs e) 
        {
        
        }

        static void OnGyroscopeData(object sender, GyroscopeDataEventArgs e)
        {

        }

        static void OnOrientationData(object sender, OrientationDataEventArgs e)
        {

        }

        static void OnPoseChanged(object sender, PoseChangedEventArgs e)
        {

        }

        static void OnRecognizedArm(object sender, RecognizedArmEventArgs e)
        {

        }

        static void OnLostArm(object sender, MyoEventArgs e)
        {

        }

    }
}
