using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Wrapper;
using Myomi.Analyzer;
using Myomi.Data;
using MyoNet.Myo;

namespace Myomi.Task
{
    class GestureCreatorMatchingTask: ITaskHandler, IGestureMatch
    {
        public MyomiMyo Myo { get; set; }
        public bool Matched { get; private set; }

        //part of the IGestureMatch interface 
        public MyoDataAnalyzer Analyzer { get; set; }
        public List<MyoDataProfile> ToMatch { get; set; }
        public MyomiGestureOptions ToMatchOptions { get; set; }
        public int ToMatchFrameIndex { get; set; }
        public int CurrentFrameRepeat { get; set; }

        public GestureCreatorMatchingTask(List<MyoDataProfile> toMatch, MyomiGestureOptions toMatchOptions) 
        {
            this.ToMatch = toMatch;
            this.ToMatchOptions = toMatchOptions;
            this.ToMatchFrameIndex = 0;
            this.CurrentFrameRepeat = 0;
        }

        //need to refactor
        //have a split for analysis between last frame and all other frames
        public bool Handle(MyoDataAnalyzer analyzer)
        {
            this.Analyzer = analyzer;
            //check to see if this is the last frame or not
            if (ToMatchFrameIndex + 1 == ToMatch.Count)
            {
                var result = GestureMatchingEngine.LastFrameAnalyze(this);
                if (result == LastFrameResult.Matched)
                {
                    return MatchingSuccess();
                }
                else if (result == LastFrameResult.Failed)
                {
                    return MatchingFailed();
                }
                else
                {
                    return false;
                }
            }
            else 
            {
                if (GestureMatchingEngine.FrameAnalyze(this))
                {
                    return false;
                }
                else
                {
                    return MatchingFailed();
                }
            }
        }

        private bool MatchingFailed()
        {
            Console.WriteLine("Matching has failed");
            Myo.Vibrate(VibrationType.Short);
            this.Matched = false;
            return true;
        }

        private bool MatchingSuccess() 
        {
            Console.WriteLine("Matching success!");
            Myo.Vibrate(VibrationType.Medium);
            this.Matched = true;
            return true;
        }
    }
}
