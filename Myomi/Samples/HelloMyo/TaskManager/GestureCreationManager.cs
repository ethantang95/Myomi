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
                AnalyzeData(data);
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

        void AnalyzeData(List<MyoData> data) 
        {
        
        }
    }
}
