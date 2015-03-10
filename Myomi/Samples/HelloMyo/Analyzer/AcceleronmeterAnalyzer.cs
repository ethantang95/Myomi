using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Data;

namespace Myomi.Analyzer
{
    class AcceleronmeterAnalyzer
    {
        public AcceleronmeterData Data { get; private set; }

        public AcceleronmeterAnalyzer(AcceleronmeterRawData rawData) 
        {
            TranslateRaw(rawData);
        }

        private void TranslateRaw(AcceleronmeterRawData rawData)
        {
            double upperBound = Context.Instance.UserCalibration.FastAccel * 0.6;
            double lowerBound = Context.Instance.UserCalibration.SlowAccel * 0.6;

            Data.X = CalculateThreashold(upperBound, lowerBound, rawData.X);
            Data.Y = CalculateThreashold(upperBound, lowerBound, rawData.Y);
            Data.Z = CalculateThreashold(upperBound, lowerBound, rawData.Z);
            Data.Normal = CalculateThreashold(Context.Instance.UserCalibration.FastAccel, Context.Instance.UserCalibration.SlowAccel, rawData.Normal);
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
