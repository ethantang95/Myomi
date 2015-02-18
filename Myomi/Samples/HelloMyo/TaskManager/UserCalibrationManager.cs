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
                _complete &= AnalyzeResults();
            }
        }

        bool GetMaxAccel() 
        {
            Console.WriteLine("Swing your arms in a fast motion to have a upper bound acceleration calibration");
            Console.WriteLine("Press enter to start");
            Console.ReadLine();

            try
            {
                var task = GetData(UserCalibrationTask.Calibrating.MaxAccel);
                _maxAccel = task.GetDesired();
                Console.WriteLine("Calibration Success");
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
            Console.WriteLine("Move your arms in a slow motion to have a lower bound acceleration calibration");
            Console.WriteLine("Press enter to start");
            Console.ReadLine();

            try
            {
                var task = GetData(UserCalibrationTask.Calibrating.MinAccel);
                _minAccel = task.GetDesired();
                Console.WriteLine("Calibration Success");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong when trying to calibrate user movements");
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        bool GetMaxGyro()
        {
            Console.WriteLine("Rotate your wrist in a fast motion to have a upper bound rotation calibration");
            Console.WriteLine("Press enter to start");
            Console.ReadLine();

            try
            {
                var task = GetData(UserCalibrationTask.Calibrating.MaxGyro);
                _maxGyro = task.GetDesired();
                Console.WriteLine("Calibration Success");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong when trying to calibrate user movements");
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        bool GetMinGyro()
        {
            Console.WriteLine("Rotate your wrist in a slow motion to have a lower bound rotation calibration");
            Console.WriteLine("Press enter to start");
            Console.ReadLine();

            try
            {
                var task = GetData(UserCalibrationTask.Calibrating.MinGyro);
                _minGyro = task.GetDesired();
                Console.WriteLine("Calibration Success");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong when trying to calibrate user movements");
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        UserCalibrationTask GetData(UserCalibrationTask.Calibrating currentlyCalibrating) 
        {
            var task = new UserCalibrationTask(currentlyCalibrating);
            var manager = new MyomiTaskManager((1000 / 20), task);
            var taskThread = new Thread(manager.Run);

            //we are giving the users 5 seconds to calibrate their arm motion
            taskThread.Start();
            CommonOperations.Sleep(5000);

            manager.StopExecution = true;
            return task;
        }

        private bool AnalyzeResults()
        {
            throw new NotImplementedException();
        }
    }
}
