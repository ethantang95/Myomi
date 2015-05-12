using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Data;

namespace Myomi.Analyzer
{

    //the results are simply used to see what kind of matching it is
    //Match, NoMatch, and NotAnalyzed are straight forward
    //SlightMatch is if it matches with an adjacent level, NotRest is when a rest is detected/expected and obtained is not that
    //this class is made to match 1 single instance
    enum Result { Match, SlightMatch, NoMatch, NotRest, NotAnalyzed }

    internal class MyoDataAnalyzer
    {
        private PoseAnalyzer _poseAnalyzer;
        private AcceleronmeterAnalyzer _accelAnalyzer;
        private GyroscopeAnalyzer _gyroAnalyzer;
        private OrientationAnalyzer _orienAnalyzer;

        public MyoRawData RawData { get; private set; }
        public MyoData Data { get; private set; }

        public MyoDataAnalyzer(MyoRawData rawData) 
        {
            RawData = rawData;
            _poseAnalyzer = new PoseAnalyzer(RawData.Pose);
            _accelAnalyzer = new AcceleronmeterAnalyzer(RawData.Accel);
            _gyroAnalyzer = new GyroscopeAnalyzer(RawData.Gyro);
            _orienAnalyzer = new OrientationAnalyzer(RawData.Orien);
            AnalyzeFromRaw();
        }
        public int Evaluator(MyoDataProfile toCompare, MyomiGestureOptions options, int initialScore)
        {
            //initial score should be 100 unless there's frame mismatches
            int score = initialScore;
            score -= _poseAnalyzer.GetPoint(toCompare.Pose, options);
            score -= _accelAnalyzer.GetPoint(toCompare.Accel, options);
            score -= _gyroAnalyzer.GetPoint(toCompare.Gyro, options);
            score -= _orienAnalyzer.GetPoint(toCompare.Orien, options);
            return score;
        }

        //this will then analyze the raw data and parse it into a myo data class
        //we do not want to make it all automatic because we have different tasks asking for different data
        private void AnalyzeFromRaw()
        {
            Data = new MyoData();
            Data.Pose = _poseAnalyzer.Data;
            Data.Accel = _accelAnalyzer.Data;
            Data.Gyro = _gyroAnalyzer.Data;
            Data.Orien = _orienAnalyzer.Data;
        }
    }
}
