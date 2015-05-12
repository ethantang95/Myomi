using Myomi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myomi.Analyzer
{
    class OrientationAnalyzer
    {
        public OrientationData Data { get; private set; }

        public OrientationAnalyzer(OrientationRawData rawData) 
        {
            TranslateRaw(rawData);
        }
        private void TranslateRaw(OrientationRawData rawData)
        {
            Data.Pitch = CalculateThreashold(rawData.Pitch);
            Data.Roll = CalculateThreashold(rawData.Roll);
            Data.Azimuth = CalculateThreashold(rawData.Azimuth);
        }
        private int CalculateThreashold(double source) 
        {
            if (source >= 0 && source < 90) 
            {
                return 1;
            }
            else if (source >= 90 && source < 180) 
            {
                return 2;
            }
            else if (source >= 180 && source < 270)
            {
                return 3;
            }
            else 
            {
                return 4;
            }
        }

        private Result Analyze(int profileValue, int dataValue) 
        {
            if (profileValue == dataValue)
            {
                return Result.Match;
            }
            else if (Math.Abs(profileValue - dataValue) == 2)
            {
                return Result.NoMatch;
            }
            else 
            {
                return Result.SlightMatch;
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
                    return Context.Instance.Points.SlightMatchOriental;
                case Result.NoMatch:
                    return Context.Instance.Points.NoMatchOrientation;
                default:
                    Console.WriteLine("Invalid result of {0} for acceleronmeter", result);
                    return 100;
            }
        }

        public int GetPoint(OrientationProfileData profile, MyomiGestureOptions options) 
        {
            if (!options.OrienEnabled) 
            {
                return CalculatePoint(Result.NotAnalyzed);
            }

            if (options.OrienHalfMode) 
            {
                Data.Pitch = CommonOperations.GetHalfModeValue(Data.Pitch); 
                Data.Roll = CommonOperations.GetHalfModeValue(Data.Roll); 
                Data.Azimuth = CommonOperations.GetHalfModeValue(Data.Azimuth); 
            }

            int points = 0;
            var result = Analyze(profile.Pitch, Data.Pitch);
            points += CalculatePoint(result);
            result = Analyze(profile.Roll, Data.Roll);
            points += CalculatePoint(result);
            result = Analyze(profile.Azimuth, Data.Azimuth);
            points += CalculatePoint(result);
            return points;
        }
    }
}
