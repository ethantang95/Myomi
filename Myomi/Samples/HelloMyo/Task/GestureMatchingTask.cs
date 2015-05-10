using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Analyzer;
using Myomi.Data;
using Myomi.Wrapper;

namespace Myomi.Task
{
    //this class does the main purpose of myomi, which is take the data and matches it with profiles
    internal class GestureMatchingTask: ITaskHandler
    {
        private class GestureState : IGestureMatch
        {
            public MyoDataAnalyzer Analyzer { get; set; }
            public List<MyoDataProfile> ToMatch { get; set; }
            public MyomiGestureOptions ToMatchOptions { get; set; }
            public MyomiGestureAction ToMatchAction { get; set; }
            public int ToMatchFrameIndex { get; set; }
            public int CurrentFrameRepeat { get; set; }
            public bool Matched { get; set; }
        }

        public MyomiMyo Myo { get; set; }
        
        private MyoDataAnalyzer _analyzer;
        private List<GestureState> _gestureStates;

        public GestureMatchingTask()
        {
            this._gestureStates = new List<GestureState>();
            PopulateGestures();
        }

        public bool Handle(MyoDataAnalyzer analyzer)
        {
            this._analyzer = analyzer;

            //first, we analyze which ones we are removing due to not matching the criteria
            this._gestureStates.RemoveAll(MatchingAnalysis);

            //now, depending on the setting, we will see what gets done
            if (this._gestureStates.Count == 1)
            {
                if (Context.Instance.CurrentProfile.Options.FastActivation || this._gestureStates.First().Matched)
                {
                    this._gestureStates.First().ToMatchAction.ExecuteAction();
                    PopulateGestures();
                }
            }
            else if (this._gestureStates.Count == 0)
            {
                PopulateGestures();
            }
            else
            {
                //the first one to be matched will have its actions executed
                var matched = this._gestureStates.Find(gesture => gesture.Matched);
                matched.ToMatchAction.ExecuteAction();
                PopulateGestures();
            }

            return false;
        }

        //return true if not matched, else return false
        private bool MatchingAnalysis(GestureState gesture)
        {
            if (gesture.ToMatchFrameIndex + 1 == gesture.ToMatch.Count)
            {
                var result = GestureMatchingEngine.LastFrameAnalyze(gesture);
                if (result == LastFrameResult.Matched)
                {
                    gesture.Matched = true;
                    return false;
                }
                else if (result == LastFrameResult.Failed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (GestureMatchingEngine.FrameAnalyze(gesture))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void PopulateGestures()
        {
            foreach (var gesture in Context.Instance.CurrentProfile.Gestures)
            {
                var gestureState = new GestureState
                {
                    Analyzer = this._analyzer,
                    ToMatch = gesture.SegmentsWithOptions,
                    ToMatchOptions = gesture.Options,
                    ToMatchAction = gesture.Action,
                    ToMatchFrameIndex = 0,
                    CurrentFrameRepeat = 0,
                    Matched = false,
                };
                this._gestureStates.Add(gestureState);
            }
        }
    }
}
