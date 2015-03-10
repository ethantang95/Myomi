using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Data;

namespace Myomi.Analyzer
{
    class GyroscopeAnalyzer
    {
        public GyroscopeData Data { get; private set; }

        public GyroscopeAnalyzer(GyroscopeRawData rawData) 
        {
            TranslateRaw(rawData);
        }
        private void TranslateRaw(GyroscopeRawData rawData)
        {
            double upperBound = Context.Instance.UserCalibration.FastGyro * 0.6;
            double lowerBound = Context.Instance.UserCalibration.SlowGyro * 0.6;

            Data.X = CalculateThreashold(upperBound, lowerBound, rawData.X);
            Data.Y = CalculateThreashold(upperBound, lowerBound, rawData.Y);
            Data.Z = CalculateThreashold(upperBound, lowerBound, rawData.Z);
            Data.Normal = CalculateThreashold(Context.Instance.UserCalibration.FastGyro, Context.Instance.UserCalibration.SlowGyro, rawData.Normal);
        }

        //I might have a different calculation for gyro as compared to acceleration
        //so I am having this duplicated... this is because works in a wierd way right now
        private int CalculateThreashold(double upperBound, double lowerBound, double value)
        {
            //threashold 1 is if it's above lowerbound but below halfway of the average of both bounds
            //threashold 2 is if it's above the halfway but lower than the upperbound
            if (value > upperBound)
                return 3;
            else if (value < lowerBound)
                return 0;
            else if (value >= lowerBound && value < (upperBound + lowerBound) / 2)
                return 1;
            else
                return 2;
        }
    }
}
