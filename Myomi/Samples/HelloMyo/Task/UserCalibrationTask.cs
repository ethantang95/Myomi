using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Analyzer;
using Myomi.Data;
using Myomi.Wrapper;
using MyoNet.Myo;

namespace Myomi.Task
{

    //this class is for calculating the user calibration, so just raw data is needed
    internal class UserCalibrationTask: ITaskHandler
    {
        //this will be used for identifying which calibration are we doing
        public enum Calibrating { FastAccel, SlowAccel, FastGyro, SlowGyro, Arm }

        public MyomiMyo Myo { get; set; }

        List<double> _collectedValues;
        Arm _arm;
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
                case Calibrating.FastAccel:
                    _collectedValues.Add(rawData.Accel.Normal);
                    break;
                case Calibrating.SlowAccel:
                    _collectedValues.Add(rawData.Accel.Normal);
                    break;
                case Calibrating.FastGyro:
                    _collectedValues.Add(rawData.Gyro.Normal);
                    break;
                case Calibrating.SlowGyro:
                    _collectedValues.Add(rawData.Gyro.Normal);
                    break;
                case Calibrating.Arm:
                    _arm = Myo.GetCurrentArm();
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
                case Calibrating.FastAccel:
                    return 0.8 * valuesArray.Max();
                case Calibrating.SlowAccel:
                    return 1.2 * valuesArray.Max();
                case Calibrating.FastGyro:
                    return 0.8 * valuesArray.Max();
                case Calibrating.SlowGyro:
                    return 1.0 * valuesArray.Max();
                default:
                    //should never reach here
                    return 0;
            }
        }

        public Arm GetArm() 
        {
            return _arm;
        }
    }
}
