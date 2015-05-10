using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Myomi.Data
{
    internal class MyomiGesture
    {
        List<MyoDataProfile> _segments;
        public List<MyoDataProfile> SegmentsWithOptions { get; set; }
        public MyomiGestureOptions Options { get; set; }
        public MyomiGestureAction Action { get; set; }
        public string Name { get; set; }
        public Guid ID { get; set; }

        //this constructor should only be called when a new gesture is created, therefore, a new GUID will be used to identify it
        //we will pass the raw data collected from a gesture creation task sent by the myomi profile class
        public MyomiGesture(MyomiGestureOptions options, List<MyoDataProfile> segments) 
        {
            this._segments = segments;
            this.Options = options;
            GenerateFramesWithOptions();
            this.ID = Guid.NewGuid();
        }

        public MyomiGesture(Guid gestureID, Guid profileID)
        {
            this.ID = gestureID;
            CreateGestureFromFile(profileID);
        }

        public void GenerateFramesWithOptions() 
        {
            var generated = new List<MyoDataProfile>();
            MyoDataProfile prevSegment = null;
            foreach (var segment in _segments) 
            {
                var frameWithOptions = GenerateFramesWithOptions(segment);
                if (prevSegment == null || frameWithOptions.CompareTo(prevSegment) == 0) 
                {
                    generated.Add(frameWithOptions);
                    prevSegment = frameWithOptions;
                }
                generated.Last().Frames += segment.Frames;
            }
            //now, we want to clean up the frames if the option is enabled
            if (Options.IgnoreFrameCounts) 
            {
                foreach (var segment in generated) 
                {
                    if (segment != generated.Last()) 
                    {
                        segment.Frames = 0;
                    }
                }
            }
            this.SegmentsWithOptions = generated;
        }

        public MyoDataProfile GenerateFramesWithOptions(MyoDataProfile toAnalyze) 
        {
            var toReturn = new MyoDataProfile();
            //pose
            if (Options.PoseEnabled)
            {
                toReturn.Pose = (PoseProfileData)toAnalyze.Pose.Clone();
            }
            else 
            {
                toReturn.Pose.SetToNone();
            }

            //acceleronometer
            if (Options.AccelNormOnly) 
            {
                toReturn.Accel.Normal = toAnalyze.Accel.Normal;
            }
            else if (Options.AccelEnabled)
            {
                toReturn.Accel = (AcceleronmeterProfileData)toAnalyze.Accel.Clone();
            }
            else 
            {
                toReturn.Accel.SetToNone();
            }

            //gyroscope
            if (Options.GyroNormOnly)
            {
                toReturn.Gyro.Normal = toAnalyze.Gyro.Normal;
            }
            else if (Options.AccelEnabled)
            {
                toReturn.Gyro = (GyroscopeProfileData)toAnalyze.Gyro.Clone();
            }
            else
            {
                toReturn.Gyro.SetToNone();
            }

            //orientation
            if (Options.OrienHalfMode)
            {
                toReturn.Orien.Azimuth = CommonOperations.GetHalfModeValue(toAnalyze.Orien.Azimuth);
                toReturn.Orien.Roll = CommonOperations.GetHalfModeValue(toAnalyze.Orien.Roll);
                toReturn.Orien.Pitch = CommonOperations.GetHalfModeValue(toAnalyze.Orien.Pitch);
            }
            else if (Options.OrienEnabled)
            {
                toReturn.Orien = (OrientationProfileData)toAnalyze.Orien.Clone();
            }
            else 
            {
                toReturn.Orien.SetToNone();
            }

            return toReturn;
        }

        public void SaveGesture(Guid profileID)
        {
            var toWrite = new List<string>();
            toWrite.Add(this.Options.ToFileString());
            foreach (var gesture in _segments) 
            {
                toWrite.Add(gesture.ToFileString());
            }
            toWrite.Add(this.Action.ToFileString());
            FileHelper.WriteToFile(toWrite, Path.Combine("Profiles", profileID.ToString(), (this.ID.ToString() + ".mygest")));
        }

        private void CreateGestureFromFile(Guid profileID)
        {
            //first, we use file helper to get us all the contents
            var contents = FileHelper.GetContentFromFile(Path.Combine("Profiles", profileID.ToString(), this.ID.ToString()));
            //the way it is written is that the first line will always be the name
            this.Name = contents.First();
            contents.RemoveAt(0);
            //the next line would be the options
            //all the other lines will be the gesture without options, we can generate the gesture with options here
            this.Options = new MyomiGestureOptions(contents.First());
            //remove the options line now
            contents.RemoveAt(0);
            foreach (var line in contents)
            {
                try
                {
                    var segment = new MyoDataProfile(line);
                    _segments.Add(segment);
                }
                catch (Exception e)
                {
                    Console.WriteLine("something is wrong with the gesture file of {0}", this.ID.ToString());
                    //if the guid is empty, we set it to an empty value and ready it for deletion or something
                    this.ID = Guid.Empty;
                    return;
                }
            }
            this.Action = new MyomiGestureAction(contents.Last());
            GenerateFramesWithOptions();
        }
    }
}
