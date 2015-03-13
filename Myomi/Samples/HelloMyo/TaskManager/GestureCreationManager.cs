using Myomi.Data;
using Myomi.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
            if (keep)
            {
                var options = SetOptions();
                AnalyzeData(data, options);
            }
            else 
            {
                Console.WriteLine("Would you wish to try again?");
                bool tryAgain = CommonOperations.GetYesOrNo();
                if (tryAgain)
                {
                    //recursive call... ok, it's pretty lazy I have to admit
                    this.Run();
                }
                else 
                {
                    return;
                }
            }

        }

        List<MyoData> GetData() 
        {
            var task = new GestureCreatorTask();
            var manager = new MyomiTaskManager((1000 / 20), task);
            var taskThread = new Thread(manager.Run);
            taskThread.Start();

            Console.WriteLine("To start recording, please perform the double tap gesture");
            Console.WriteLine("To finish recording, please perform the double tap gesture again and press enter");
            Console.ReadLine();

            manager.StopExecution = true;
            return task.GetData();
        }


        MyomiProfileOptions SetOptions() 
        {
            var options = new MyomiProfileOptions();
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
        void EnableComponents(string component) 
        {
            Console.WriteLine("Would you like to enable {0}?", component);
        }

        void SetAsNormal(string component) 
        {
            Console.WriteLine("Would you like to use the normal component of {0} only?", component);
        }

        //here, we create a myomi gesture profile from the data and options given
        void AnalyzeData(List<MyoData> data, MyomiProfileOptions options) 
        {
            foreach (var dataFrame in data) 
            {
                
            }
        }
    }
}
