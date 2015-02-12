using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyoNet.Myo;

namespace Myomi.Wrapper
{
    class MyomiHub
    {
        Hub hub;

        public void Initialize()
        {
            hub = new Hub("com.myomi.program");
            hub.LockingPolicy = MyoNet.Myo.LockingPolicy.None;
            hub.MyoUnpaired += OnUnpair;
        }

        //here, we should clean up the left over data or do something when it is unpaired
        private void OnUnpair(object sender, MyoEventArgs e) 
        {

        }

        internal IMyo GetMyo()
        {
            return hub.WaitForMyo();
        }
    }
}
