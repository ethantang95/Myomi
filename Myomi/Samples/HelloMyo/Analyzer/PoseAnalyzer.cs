using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Data;
using MyoNet.Myo;

namespace Myomi.Analyzer
{
    class PoseAnalyzer
    {
        public PoseData Data { get; private set; }

        public PoseAnalyzer(PoseRawData rawData) 
        {
            TranslateRaw(rawData);
        }

        private void TranslateRaw(PoseRawData rawData)
        {
            Data = new PoseData();
            Data.Pose = rawData.Pose;
        }
        private Result Analyze(PoseProfileData profile, MyomiProfileOptions options)
        { 
            //we might have an edge or soft mode where a swift transition from one pose to another will have a leeway time
            if (!options.PoseEnabled)
                return Result.NotAnalyzed;

            if (profile.Pose == this.Data.Pose)
                return Result.Match;
            else
            {
                if (profile.Pose == Pose.Rest || this.Data.Pose == Pose.Rest)
                {
                    return Result.NotRest;
                }
                else
                {
                    return Result.NoMatch;
                }
            }
        }

        public int GetPoint(PoseProfileData profile, MyomiProfileOptions options) 
        {
            Result result = Analyze(profile, options);
            switch (result)
            {
                case Result.NotAnalyzed:
                    return Context.Instance.Points.NotAnalyzedPose;
                case Result.Match:
                    return Context.Instance.Points.Match;
                case Result.NotRest:
                    return Context.Instance.Points.NotRestPose;
                case Result.NoMatch:
                    return Context.Instance.Points.NoMatchPose;
                default:
                    //should never happen, but if it does, count it as invalid
                    Console.Write("Invalid result of {0} for pose", result);
                    return 100;
            }
        }
    }
}
