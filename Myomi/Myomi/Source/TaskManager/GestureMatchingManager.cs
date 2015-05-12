using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Myomi.Task;

namespace Myomi.TaskManager
{
    class GestureMatchingManager
    {
        public void Run() 
        {
            Console.WriteLine("The program is now running profile {0}", Context.Instance.CurrentProfile.Name);
            Console.WriteLine("Please press enter to start");
            Console.ReadLine();
            var task = new GestureMatchingTask();
            var manager = new MyomiTaskManager((1000 / 20), task);
            var taskThread = new Thread(manager.Run);
            taskThread.Start();
            
            Console.WriteLine("To stop, press enter");
            Console.ReadLine();
            manager.StopExecution = true;

            Console.WriteLine("The task has stopped, returning back to main menu");
        }
    }
}
