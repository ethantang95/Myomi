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

        static MyomiMyo _myomiMyo;
        IMyo _myo;
        MyoState _myoState;
        //not sure if this is dangerous or not... we do not know the beahviour of recog/lostArm events
        //it might put the program in an infinite stuck loop if the behaviour is not what is assumed
        bool _isOnArm;

        //this should only be set internally for now... unless there's a task where we need to set it externally
        public bool InstanceCollectionEnabled { get; private set; }
        public bool MyoFound { get; private set; }

        MyomiMyo() 
        {
            Create();
        }
        void Create()
        {
            _myoState = new MyoState();
            MyoFound = false;
            for (int i = 0; i < 30 && _myo == null; i++)
            {
                Console.Write("\r");
                Console.Write("Attempting to find a myo, attempt {0}", i);
                _myo = Context.Instance.Hub.GetMyo();
            }
            if (_myo == null) 
            {
                return;
            }

            MyoFound = true;
            //we will vibrate whenever a new MyomiMyo is created

            _myo.AccelerometerDataAcquired += OnAccelerometerData;
            _myo.GyroscopeDataAquired += OnGyroscopeData;
            _myo.OrientationDataAcquired += OnOrientationData;
            _myo.PoseChanged += OnPoseChanged;
            _myo.RecognizedArm += OnRecognizedArm;
            _myo.LostArm += OnLostArm;

            if (_isOnArm)
            {
                InstanceCollectionEnabled = true;
            }
        }

        static public MyomiMyo Initialize() 
        {
            if (_myomiMyo == null)
            {
                _myomiMyo = new MyomiMyo();
            }
            _myomiMyo.Vibrate(VibrationType.Medium);
            _myomiMyo.InstanceCollectionEnabled = true;
            return _myomiMyo;
        }
        //not actually destroy it... but kinda just render it useless as somehow, we cannot get another one
        public void Terminate() 
        {
            Vibrate(VibrationType.Medium);
            InstanceCollectionEnabled = false;
        }

        public Arm GetCurrentArm() 
        {
            return _myoState.Arm;
        }

        public MyoRawData GetCurrentData() 
        {
            MyoRawData data = new MyoRawData(_myoState);
            return data;
        }

        public void Vibrate(VibrationType vibType) 
        {
            _myo.Vibrate(vibType);
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
            MyoStateNotification();
            _myoState.Arm = e.Arm;
            _isOnArm = true;
            InstanceCollectionEnabled = true;
        }

        void OnLostArm(object sender, MyoEventArgs e)
        {
            MyoStateNotification();
            _isOnArm = false;
            InstanceCollectionEnabled = false;
        }
        #endregion

        void MyoStateNotification() 
        {
            Vibrate(VibrationType.Short);
            CommonOperations.Sleep(150);
            Vibrate(VibrationType.Short);
        }
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
