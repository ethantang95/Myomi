using Myomi.Analyzer;
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

        public bool StopExecution { get; set; }

        public MyomiTaskManager(double frequency, ITaskHandler handler) 
        {
            this._frequency = frequency;
            this._handler = handler;
        }

        //this shall be the method to be called for async task
        public void Run()
        {
            while (!StopExecution)
            {
                Context.Instance.Hub.RunHub(_frequency);
                this.Analyze();
            }
        }

        private void Analyze()
        {
            var InstanceAnalyzer = new MyoDataAnalyzer();
            _handler.Handle(InstanceAnalyzer);
        }
    }
}
