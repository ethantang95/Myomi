using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyoNet.Myo;

namespace Myomi.Wrapper
{
    internal class MyomiHub
    {
        Hub hub;

        public MyomiHub() 
        {
            Initialize();
        }
        public void Initialize()
        {
            hub = new Hub("com.myomi.program");
            hub.LockingPolicy = MyoNet.Myo.LockingPolicy.None;
            hub.MyoUnpaired += MyoGone;
            hub.MyoDisconnected += MyoGone;
            hub.MyoPaired += MyoAppear;
            hub.MyoConnected += MyoAppear;
        }

        internal IMyo GetMyo()
        {
            return hub.WaitForMyo(TimeSpan.FromMilliseconds(1500));
        }

        public void RunHub(double frequency) 
        {
            hub.Run(TimeSpan.FromMilliseconds(frequency));
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
