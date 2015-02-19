using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyoNet.Myo;

namespace Myomi.Wrapper
{
    internal class MyomiHub
    {
        Hub _hub;
        IMyo _myo;

        public MyomiHub() 
        {
            Initialize();
        }
        public void Initialize()
        {
            if (_hub == null)
            {
                _hub = new Hub("com.myomi.program");
                //hub.LockingPolicy = MyoNet.Myo.LockingPolicy.None;
                _hub.MyoUnpaired += MyoGone;
                _hub.MyoDisconnected += MyoGone;
                _hub.MyoPaired += MyoAppear;
                _hub.MyoConnected += MyoAppear;
            }
        }

        internal IMyo GetMyo()
        {
            if (_myo == null)
            {
                _myo = _hub.WaitForMyo(TimeSpan.FromMilliseconds(1500));
                return _myo;
            }
            else 
            {
                return _myo;
            }
        }

        public void RunHub(double frequency) 
        {
            _hub.Run(TimeSpan.FromMilliseconds(frequency));
        }

        //here, we should clean up the left over data or do something when it is unpaired
        void MyoGone(object sender, MyoEventArgs e) 
        {
            //first, halt all operations
            Context.Instance.GlobalTaskHalt = true;
        }

        void MyoAppear(object sender, MyoEventArgs e) 
        {
            Context.Instance.GlobalTaskHalt = false;
        }
    }
}
