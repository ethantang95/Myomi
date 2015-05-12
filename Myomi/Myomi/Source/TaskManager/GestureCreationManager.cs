using Myomi.Data;
using Myomi.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading;
using WindowsInput.Native;

namespace Myomi.TaskManager
{
    internal class GestureCreationManager
    {
        public void Run() 
        {
            Console.WriteLine("Welcome to the gesture creation");
            Console.WriteLine("To initalize a gesture pattern, please press enter");
            Console.ReadLine();

            var data = GetData();

            Console.WriteLine("Do you wish to keep this gesture?");
            bool keep = CommonOperations.GetYesOrNo();
            bool calibrated = false ;
            bool tryAgain = false;
            if (keep)
            {
                var options = SetOptions();
                var gesture = AnalyzeData(data, options);
                //here, we want to make the user repeat the action a few times to see how comfortable it is with the settings
                do
                {
                    calibrated = CalibrateGesture(gesture, options);
                    if (!calibrated)
                    {
                        Console.WriteLine("Would you wish to try calibrating again?");
                        tryAgain = CommonOperations.GetYesOrNo();
                    }
                    else 
                    {
                        //we have succeed calibration
                        tryAgain = false;
                        SetAction(gesture);
                        SaveGesture(gesture);
                    }
                } while (tryAgain);
            }
            //if the user wants to make a new gesture
            if (!keep && !calibrated && !tryAgain)
            {
                Console.WriteLine("Would you wish to still create a new gesture?");
                //reusing variables ftw
                tryAgain = CommonOperations.GetYesOrNo();
                if (tryAgain)
                {
                    //recursive call... ok, it's pretty lazy I have to admit. Might be a problem
                    this.Run();
                }
                else 
                {
                    return;
                }
            }
        }

        private List<MyoData> GetData() 
        {
            var task = new GestureCreatorTask();
            var manager = new MyomiTaskManager((1000 / 20), task);
            var taskThread = new Thread(manager.Run);
            taskThread.Start();

            Console.WriteLine("To start recording, please perform the double tap gesture at rest");
            Console.WriteLine("To finish recording, please perform the double tap gesture at rest");
            //we will put this thread in an infinite loop until it has finished
            while (!manager.StopExecution) { }

            return task.GetData();
        }

        private MyomiGestureOptions SetOptions() 
        {
            var options = new MyomiGestureOptions();
            Console.WriteLine("Would you like to change any of the default configurations for this gesture?");
            if (CommonOperations.GetYesOrNo()) 
            {
                return options;
            }

            EnableComponents("pose");
            options.PoseEnabled = CommonOperations.GetYesOrNo();
            EnableComponents("acceleration");
            options.AccelEnabled = CommonOperations.GetYesOrNo();
            EnableComponents("gyroscope");
            options.GyroEnabled = CommonOperations.GetYesOrNo();
            EnableComponents("orientation");
            options.OrienEnabled = CommonOperations.GetYesOrNo();

            SetAsNormal("acceleration");
            options.AccelNormOnly = CommonOperations.GetYesOrNo();
            SetAsNormal("gyroscope");
            options.GyroNormOnly = CommonOperations.GetYesOrNo();

            Console.WriteLine("Would you like to use half mode for orientation only?");
            Console.WriteLine("Half mode will only split orientation into 2 halves instead of 4 quarters for processing.");
            options.OrienHalfMode = CommonOperations.GetYesOrNo();

            Console.WriteLine("Would you like to ignore the frame counts for this gesture?");
            options.IgnoreFrameCounts = CommonOperations.GetYesOrNo();

            return options;
        }

        //code factor to the extreme lol...
        private void EnableComponents(string component) 
        {
            Console.WriteLine("Would you like to enable {0}?", component);
        }

        private void SetAsNormal(string component) 
        {
            Console.WriteLine("Would you like to use the normal component of {0} only?", component);
        }

        //here, we create a myomi gesture profile from the data and options given
        private MyomiGesture AnalyzeData(List<MyoData> data, MyomiGestureOptions options) 
        {
            //the idea is that we are comparing it frame by frame
            var profileFrames = new List<MyoDataProfile>();
            //we want to isolate the first frame for comparison, the prev frame will be the first unique frame listed
            var prevFrame = data.First();
            profileFrames.Add(MyoDataProfile.ConvertToProfile(prevFrame));
            data.RemoveAt(0);
            //this is just original data profiling, later, we need to create a new profile based on the options
            foreach (var dataFrame in data) 
            {
                //returning 1 would mean they are the same
                if (prevFrame.CompareTo(dataFrame) == 1)
                {
                    profileFrames.Last().Frames++;
                }
                else 
                {
                    //they are different, so we have a new frame set, we want this as the unique frame now
                    profileFrames.Add(MyoDataProfile.ConvertToProfile(dataFrame));
                    prevFrame = dataFrame;
                }
            }
            return new MyomiGesture(options, profileFrames);
        }

        private bool CalibrateGesture(MyomiGesture gesture, MyomiGestureOptions options)
        {
            var task = new GestureCreatorMatchingTask(gesture.SegmentsWithOptions, options);
            var manager = new MyomiTaskManager(Context.Instance.DefaultFrequency, task);

            for (int i = 0; i < 3 || task.Matched; i++) 
            {
                var taskThread = new Thread(manager.Run);
                taskThread.Start();
                //sleep until it has finished
                while (!manager.StopExecution) { }
                if (!task.Matched) 
                {
                    return false;
                }
            }
            return true;
        }

        private void SetAction(MyomiGesture gesture)
        {
            Console.WriteLine("What would you like to bind this gesture to?");
            Console.WriteLine("Please type the key or key name if you want to bind this to in a single word");
            Console.WriteLine("Example: binding the 'enter' key, please type 'enter', for page up, type 'pageup'");
            Console.WriteLine("For mouse keys, type in the mouse key: 'leftclick', 'rightclick'");

            VirtualKeyCode parsedBind;
            do
            {
                Console.WriteLine("Please type in the key you would like to bind this gesture to");
                var keybind = Console.ReadLine();
                parsedBind = KeyToKeyBind.ParseKey(keybind);
                if (parsedBind == VirtualKeyCode.NONAME)
                {
                    Console.WriteLine("The key bind you entered is not valid");
                }

            } while (parsedBind != VirtualKeyCode.NONAME);

            int delay = 10;
            Console.WriteLine("Would you like to add a delay (in millisecond) between key and and key down?");
            if (CommonOperations.GetYesOrNo())
            {
                string delayString = "";
                do
                {
                    Console.WriteLine("Please input the amount of delay in millisecond");
                    delayString = Console.ReadLine();
                } while (!Int32.TryParse(delayString, out delay));
            }

            gesture.Action = new MyomiGestureAction(parsedBind, delay);
        }

        private void SaveGesture(MyomiGesture gesture)
        {
            Context.Instance.CurrentProfile.AddGesture(gesture);
        }

    }
}
