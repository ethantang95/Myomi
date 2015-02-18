using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Analyzer;
using Myomi.Wrapper;

namespace Myomi.Task
{
    //this class is used for making new gestures
    internal class GestureCreatorTask: ITaskHandler
    {
        public MyomiMyo Myo { get; set; }

        MyoDataAnalyzer _analyzer;

        public void Handle(MyoDataAnalyzer analyzer)
        {
            this._analyzer = analyzer;
        }
    }
}
