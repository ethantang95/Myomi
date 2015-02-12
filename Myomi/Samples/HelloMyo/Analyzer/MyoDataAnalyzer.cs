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
        private MyoRawData _rawData;
        private MyoData _data;
        private PoseAnalyzer _poseAnalyzer;
        private AcceleronmeterAnalyzer _accelAnalyzer;
        private GyroscopeAnalyzer _gyroAnalyzer;
        private OrientationAnalyzer _orienAnalyzer;

        public MyoDataAnalyzer() 
        {
            if (!Context.Instance.Myo.InstanceCollectionEnabled)
            {
                return;
            }
            _rawData = Context.Instance.Myo.GetCurrentData();
            _poseAnalyzer = new PoseAnalyzer(_rawData.Pose);
            _accelAnalyzer = new AcceleronmeterAnalyzer(_rawData.Accel);
            _gyroAnalyzer = new GyroscopeAnalyzer(_rawData.Gyro);
            _orienAnalyzer = new OrientationAnalyzer(_rawData.Orien);
        }
        public int Evaluator(MyoData obtained, MyoDataProfile toCompare)
        {
            int score = 100;

            return score;
        }

        public MyoData AnalyzeFromRaw()
        {
            MyoData toReturn = new MyoData();


            return null;
        }
    }
}
