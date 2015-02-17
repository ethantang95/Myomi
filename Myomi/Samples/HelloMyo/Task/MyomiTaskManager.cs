using Myomi.Analyzer;
using Myomi.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myomi.Task
{
    internal class MyomiTaskManager
    {
        double _frequency;
        ITaskHandler _handler;
        MyomiMyo _myo;

        //this should be used outside to finish the current task
        public bool StopExecution { get; set; }

        public MyomiTaskManager(double frequency, ITaskHandler handler) 
        {
            this._frequency = frequency;
            this._handler = handler;
            StopExecution = false;
        }

        //this shall be the method to be called for async task
        //we want that every task, a new Myo instance is initalized, cleaning up the previous instance
        public void Run()
        {
            _myo = new MyomiMyo();
            while (!StopExecution)
            {
                Context.Instance.Hub.RunHub(_frequency);
                if (_myo.InstanceCollectionEnabled)
                {
                    this.Analyze();
                }
            }
            _myo.Terminate();
        }

        private void Analyze()
        {
            var InstanceAnalyzer = new MyoDataAnalyzer(_myo.GetCurrentData());
            _handler.Handle(InstanceAnalyzer);
        }
    }
}
