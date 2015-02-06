using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Data;
using MyoNet.Myo;

namespace Myomi.Analyzer
{
    class GestureAnalyzer
    {

        PoseProfileData profile;
        PoseData data;

        public GestureAnalyzer(PoseProfileData profile, PoseData data) 
        {
            this.profile = profile;
            this.data = data;
        }

        public Result Analyze()
        { 
            //we might have an edge or soft mode where a swift transition from one pose to another will have a leeway time
            if (this.profile.Enabled)
                return Result.NotAnalyzed;

            if (this.profile.Pose.ToString() == this.data.Pose.ToString())
                return Result.Match;
            else
                return Result.NoMatch;
        }

        public int GetPoint() 
        {
            return 0;
        }
    }
}
