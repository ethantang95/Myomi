using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Analyzer;

namespace Myomi.Task
{
    //this class does the main purpose of myomi, which is take the data and matches it with profiles
    internal class MyomiProgramTask: ITaskHandler
    {
        MyoDataAnalyzer _analyzer;

        public void Handle(MyoDataAnalyzer analyzer)
        {
            this._analyzer = analyzer;
        }
    }
}
