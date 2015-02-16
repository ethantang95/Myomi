using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Analyzer;

namespace Myomi.Task
{
    //this class is used for making new gestures
    class GestureCreatorTask: ITaskHandler
    {
        MyoDataAnalyzer _analyzer;

        public void Handle(MyoDataAnalyzer analyzer)
        {
            this._analyzer = analyzer;
        }
    }
}
