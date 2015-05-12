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

        private Result Analyze(int profileValue, int dataValue)
        {
            if (profileValue == dataValue)
            {
                return Result.Match;
            }
            else if (Math.Abs(profileValue - dataValue) == 1)
            {
                return Result.SlightMatch;
            }
            else if (!(profileValue == 0 && dataValue == 0))
            {
                return Result.NotRest;
            }
            else
            {
                return Result.NoMatch;
            }
        }

        private int CalculatePoint(Result result)
        {
            switch (result)
            {
                case Result.Match:
                    return Context.Instance.Points.Match;
                case Result.NotAnalyzed:
                    return Context.Instance.Points.NotAnalyzed;
                case Result.NotRest:
                    return Context.Instance.Points.NotRest;
                case Result.SlightMatch:
                    return Context.Instance.Points.SlightMatch;
                case Result.NoMatch:
                    return Context.Instance.Points.NoMatch;
                default:
                    Console.WriteLine("Invalid result of {0} for Gyroscope", result);
                    return 100;
            }
        }

        public int GetPoint(GyroscopeProfileData profile, MyomiGestureOptions option)
        {
            if (!option.GyroEnabled)
            {
                return CalculatePoint(Result.NotAnalyzed);
            }
            else if (option.GyroNormOnly)
            {
                var result = Analyze(profile.Normal, Data.Normal);
                return CalculatePoint(result);
            }
            //full analysis
            else
            {
                int points = 0;
                var result = Analyze(profile.X, Data.X);
                points += CalculatePoint(result) / 3;
                result = Analyze(profile.Y, Data.Y);
                points += CalculatePoint(result) / 3;
                result = Analyze(profile.Z, Data.Z);
                points += CalculatePoint(result) / 3;
                return points;
            }
        }
    }
}
