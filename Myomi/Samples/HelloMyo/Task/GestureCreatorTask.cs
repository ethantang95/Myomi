using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyoNet.Myo;
using Myomi.Analyzer;
using Myomi.Wrapper;
using Myomi.Data;

namespace Myomi.Task
{
    //this class is used for making new gestures
    internal class GestureCreatorTask: ITaskHandler
    {
        public MyomiMyo Myo { get; set; }

        MyoDataAnalyzer _analyzer;
        bool _enabled = false;
        bool _finished = false;
        List<MyoData> _dataFrames = new List<MyoData>();

        public void Handle(MyoDataAnalyzer analyzer)
        {
            this._analyzer = analyzer;
            if (ReadyState() && _enabled && !_finished) 
            {
                _finished = true;
                _enabled = false;
                Myo.Vibrate(VibrationType.Short);
            }
            if (ReadyState() && !_enabled && !_finished)
            {
                _enabled = true;
                Myo.Vibrate(VibrationType.Short);
            }
            if (_enabled) 
            {
                _dataFrames.Add((MyoData)_analyzer.Data.Clone());
            }
        }

        public List<MyoData> GetData() 
        {
            return _dataFrames;
        }

        //ready state is when the arm is still and a double tap gesture is detected
        private bool ReadyState() 
        {
            return (_analyzer.Data.Accel.Normal == 0 &&
                _analyzer.Data.Gyro.Normal == 0 &&
                _analyzer.Data.Pose.Pose == Pose.DoubleTap);
        }
    }
}
