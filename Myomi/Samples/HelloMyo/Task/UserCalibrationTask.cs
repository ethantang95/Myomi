using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Analyzer;

namespace Myomi.Task
{

    //this class is for calculating the user calibration, so just raw data is needed
    class UserCalibrationTask: ITaskHandler
    {
        //this will be used for identifying which calibration are we doing
        public enum Calibrating { MaxAccel, MinAccel, MaxGyro, MinGyro }

        MyoDataAnalyzer _analyzer;

        public void Handle(MyoDataAnalyzer analyzer)
        {
            this._analyzer = analyzer;
        }
    }
}
