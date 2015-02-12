using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyoNet.Myo;
using Myomi.Data;

namespace Myomi.Wrapper
{
    class MyomiMyo
    {

        IMyo _myo;
        MyoState _myoState;
        //not sure if this is dangerous or not... we do not know the beahviour of recog/lostArm events
        //it might put the program in an infinite stuck loop if the behaviour is not what is assumed
        bool _isOnArm;

        public bool InstanceCollectionEnabled { get; set; }

        public void Initialize()
        {
            _myoState = new MyoState();
            while (_myo == null)
            {
                _myo = Context.Instance.Hub.GetMyo();
            }
            _isOnArm = true;

            _myo.AccelerometerDataAcquired += OnAccelerometerData;
            _myo.GyroscopeDataAquired += OnGyroscopeData;
            _myo.OrientationDataAcquired += OnOrientationData;
            _myo.PoseChanged += OnPoseChanged;
            _myo.RecognizedArm += OnRecognizedArm;
            _myo.LostArm += OnLostArm;

            InstanceCollectionEnabled = true;
        }

        public MyoRawData GetCurrentData() 
        {
            MyoRawData data = new MyoRawData(_myoState);
            return data;
        }

        #region EventDeclaration
        void OnAccelerometerData(object sender, AccelerometerDataEventArgs e) 
        {
            _myoState.Accel = e.Accelerometer;
        }

        void OnGyroscopeData(object sender, GyroscopeDataEventArgs e)
        {
            _myoState.Gyro = e.Gyroscope;
        }

        void OnOrientationData(object sender, OrientationDataEventArgs e)
        {
            _myoState.Orien = e.Orientation;
        }

        void OnPoseChanged(object sender, PoseChangedEventArgs e)
        {
            _myoState.Pose = e.Pose;
        }

        void OnRecognizedArm(object sender, RecognizedArmEventArgs e)
        {
            _myoState.Arm = e.Arm;
            InstanceCollectionEnabled = true;
        }

        void OnLostArm(object sender, MyoEventArgs e)
        {
            InstanceCollectionEnabled = false;
        }
        #endregion
    }

    internal class MyoState 
    {
        public Pose Pose { get; set; }
        public Vector3 Accel { get; set; }
        public Vector3 Gyro { get; set; }
        public Quaternion Orien { get; set; }
        public Arm Arm { get; set; }

        public MyoState()
        {
            this.Pose = MyoNet.Myo.Pose.Rest;
            this.Accel = new Vector3(0, 0, 0);
            this.Gyro = new Vector3(0, 0, 0);
            this.Orien = new Quaternion(0, 0, 0, 0);
            this.Arm = MyoNet.Myo.Arm.Unknown;
        }
    }
}
