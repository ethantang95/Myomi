using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Myomi.Analyzer;
using Myomi.Data;

namespace Myomi.Task
{
    internal interface IGestureMatch
    {
        MyoDataAnalyzer Analyzer { get; set; }
        List<MyoDataProfile> ToMatch { get; set; }
        MyomiGestureOptions ToMatchOptions { get; set; }
        int ToMatchFrameIndex { get; set; }
        int CurrentFrameRepeat { get; set; }
    }

    internal enum LastFrameResult { Matched, Progressing, Failed} //matched used for matched, progressing used for frame matched but not at count, failed is for failed

    static class GestureMatchingEngine
    {
        //analyzes the frame. Returns true if it matched, false if it didn't
        public static bool FrameAnalyze(IGestureMatch matcher)
        {
            //first, calculate the initial score with the current frame
            int initialScore = 100;
            int frameDifference = matcher.CurrentFrameRepeat - (int)(matcher.ToMatch[matcher.ToMatchFrameIndex].Frames * 1.2);
            if (frameDifference > 0)
            {
                //-2 points for every frame over the tolerance limit
                initialScore -= 2 * frameDifference;
            }
            int currentFrameScore = matcher.Analyzer.Evaluator(matcher.ToMatch[matcher.ToMatchFrameIndex], matcher.ToMatchOptions, initialScore);

            //next, calculate the initial score with the next frame
            int nextFrameInitialScore = 100;
            int framesToNext = matcher.ToMatch[matcher.ToMatchFrameIndex + 1].Frames;
            if (framesToNext < 0)
            {
                nextFrameInitialScore -= -1 * 2 * framesToNext;
            }
            int nextFrameScore = matcher.Analyzer.Evaluator(matcher.ToMatch[matcher.ToMatchFrameIndex + 1], matcher.ToMatchOptions, nextFrameInitialScore);

            if (nextFrameScore > currentFrameScore && nextFrameScore > 85)
            {
                //this is the criteria to move onto the next frame
                matcher.ToMatchFrameIndex++;
                matcher.CurrentFrameRepeat = 0;
            }
            else if (currentFrameScore >= 70)
            {
                matcher.CurrentFrameRepeat++;
            }
            else
            {
                //matching failed
                return false;
            }
            return true;
        }

        public static LastFrameResult LastFrameAnalyze(IGestureMatch matcher)
        {
            //we just want to calculate the score
            var score = matcher.Analyzer.Evaluator(matcher.ToMatch[matcher.ToMatchFrameIndex], matcher.ToMatchOptions, 100);
            if (score > 70)
            {
                matcher.CurrentFrameRepeat++;
                if (matcher.CurrentFrameRepeat >= matcher.ToMatch[matcher.ToMatchFrameIndex].Frames)
                {
                    //we have a match!
                    return LastFrameResult.Matched;
                }
                else
                {
                    return LastFrameResult.Progressing;
                }
            }
            else
            {
                return LastFrameResult.Failed;
            }
        }
    }
}
