using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Data;
using Myomi.Analyzer;

namespace Myomi.Task
{
    //all the procedures that will be using the MyomiTaskManager have to pass in an ITaskHandler which will
    //carry the data taken
    interface ITaskHandler
    {
        void Handle(MyoDataAnalyzer analyzer);
    }
}
