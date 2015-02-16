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
    enum Result { Match, SlightMatch, NoMatch, NotRest, NotAnalyzed }

    internal class MyoDataAnalyzer
    {
        private PoseAnalyzer _poseAnalyzer;
        private AcceleronmeterAnalyzer _accelAnalyzer;
        private GyroscopeAnalyzer _gyroAnalyzer;
        private OrientationAnalyzer _orienAnalyzer;

        public MyoRawData RawData { get; private set; }
        public MyoData Data { get; private set; }

        public MyoDataAnalyzer() 
        {
            if (!Context.Instance.Myo.InstanceCollectionEnabled)
            {
                return;
            }
            RawData = Context.Instance.Myo.GetCurrentData();
            _poseAnalyzer = new PoseAnalyzer(RawData.Pose);
            _accelAnalyzer = new AcceleronmeterAnalyzer(RawData.Accel);
            _gyroAnalyzer = new GyroscopeAnalyzer(RawData.Gyro);
            _orienAnalyzer = new OrientationAnalyzer(RawData.Orien);
        }
        public int Evaluator(MyoData obtained, MyoDataProfile toCompare)
        {
            int score = 100;

            return score;
        }

        //this will then analyze the raw data and parse it into a myo data class
        //we do not want to make it all automatic because we have different tasks asking for different data
        public MyoData AnalyzeFromRaw()
        {
            MyoData toReturn = new MyoData();


            return null;
        }
    }
}
