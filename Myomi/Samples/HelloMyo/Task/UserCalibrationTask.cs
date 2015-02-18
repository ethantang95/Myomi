using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Analyzer;
using Myomi.Data;
using Myomi.Wrapper;

namespace Myomi.Task
{

    //this class is for calculating the user calibration, so just raw data is needed
    internal class UserCalibrationTask: ITaskHandler
    {
        //this will be used for identifying which calibration are we doing
        public enum Calibrating { MaxAccel, MinAccel, MaxGyro, MinGyro }

        public MyomiMyo Myo { get; set; }

        List<double> _collectedValues;
        Calibrating _calibrating;
        MyoDataAnalyzer _analyzer;

        public UserCalibrationTask(Calibrating calibrating) 
        {
            this._calibrating = calibrating;
            this._collectedValues = new List<double>();
        }
        public void Handle(MyoDataAnalyzer analyzer)
        {
            this._analyzer = analyzer;
            var rawData = analyzer.RawData;
            switch (_calibrating) 
            {
                case Calibrating.MaxAccel:
                    _collectedValues.Add(rawData.Accel.Normal);
                    break;
                case Calibrating.MinAccel:
                    _collectedValues.Add(rawData.Accel.Normal);
                    break;
                case Calibrating.MaxGyro:
                    _collectedValues.Add(rawData.Gyro.Normal);
                    break;
                case Calibrating.MinGyro:
                    _collectedValues.Add(rawData.Gyro.Normal);
                    break;
                default:
                    //should never reach here
                    break;
            }
        }

        public double GetDesired()
        {
            //we want to truncate the highest 5% of the values which might possibly be outliers
            _collectedValues.Sort();
            var valuesArray = _collectedValues.ToArray();
            valuesArray = valuesArray.Take((int)(valuesArray.Length * 0.95)).ToArray();
            //for max, we are setting it to 80% of the max recorded value
            //for min, we are setting it to 120% of the max recorded value for accel, 100% for gyro
            switch (_calibrating) 
            {
                case Calibrating.MaxAccel:
                    return 0.8 * valuesArray.Max();
                case Calibrating.MinAccel:
                    return 1.2 * valuesArray.Max();
                case Calibrating.MaxGyro:
                    return 0.8 * valuesArray.Max();
                case Calibrating.MinGyro:
                    return 1.0 * valuesArray.Max();
                default:
                    //should never reach here
                    return 0;
            }
        }
    }
}
