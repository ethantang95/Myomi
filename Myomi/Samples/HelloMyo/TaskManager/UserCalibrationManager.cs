using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Task;
using System.Threading;

namespace Myomi.TaskManager
{
    internal class UserCalibrationManager
    {
        bool _complete;
        double _maxAccel, _minAccel, _maxGyro, _minGyro;
        public void Run() 
        {
            CommonOperations.Sleep(2000);
            Console.Clear();
            Console.WriteLine("Starting calibration of user movements");

            while (!_complete) 
            {
                _complete &= GetMaxAccel();
                _complete &= GetMinAccel();
                _complete &= GetMaxGyro();
                _complete &= GetMinGyro();
            }
        }

        bool GetMaxAccel() 
        {
            Console.WriteLine("Swipe your arms in a fast motion to have a upper bound acceleration calibration");
            Console.WriteLine("Press enter to start");
            Console.ReadLine();

            try
            {
                var task = new UserCalibrationTask(UserCalibrationTask.Calibrating.MaxAccel);
                var manager = new MyomiTaskManager((1000 / 20), task);
                var taskThread = new Thread(manager.Run);

                //we are giving the users 5 seconds to calibrate their arm motion
                taskThread.Start();
                CommonOperations.Sleep(5000);

                manager.StopExecution = true;
                _maxAccel = task.GetDesired();
                return true;
            }
            catch (Exception e) 
            {
                Console.WriteLine("Something went wrong when trying to calibrate user movements");
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        bool GetMinAccel()
        {
            return false;
        }

        bool GetMaxGyro()
        {
            return false;
        }

        bool GetMinGyro()
        {
            return false;
        }
    }
}
