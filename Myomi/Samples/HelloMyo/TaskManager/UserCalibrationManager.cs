using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Task;
using MyoNet.Myo;
using System.Threading;

namespace Myomi.TaskManager
{
    internal class UserCalibrationManager
    {
        bool _complete;
        double _fastAccel, _slowAccel, _fastGyro, _slowGyro;
        Arm _arm;
        public void Run() 
        {
            CommonOperations.Sleep(2000);

            while (!_complete) 
            {
                Console.Clear();
                Console.WriteLine("Starting calibration of user movements");

                _complete = true;
                _complete &= GetArm();
                _complete &= GetFastAccel();
                _complete &= GetSlowAccel();
                _complete &= GetFastGyro();
                _complete &= GetSlowGyro();
                _complete &= AnalyzeResults();
            }

        }

        bool GetArm() 
        {
            Console.WriteLine("Getting which arm is the myo is on");

            try
            {
                var task = GetData(UserCalibrationTask.Calibrating.Arm, 500);
                _arm = task.GetArm();
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
        bool GetFastAccel() 
        {
            Console.WriteLine("Swing your arm back and forth in a fast motion to have a upper bound acceleration calibration");
            Console.WriteLine("Press enter to start");
            Console.ReadLine();

            try
            {
                var task = GetData(UserCalibrationTask.Calibrating.FastAccel);
                _fastAccel = task.GetDesired();
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

        bool GetSlowAccel()
        {
            Console.WriteLine("Move your arms in a slow motion to have a lower bound acceleration calibration");
            Console.WriteLine("Press enter to start");
            Console.ReadLine();

            try
            {
                var task = GetData(UserCalibrationTask.Calibrating.SlowAccel);
                _slowAccel = task.GetDesired();
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

        bool GetFastGyro()
        {
            Console.WriteLine("Rotate your wrist in a fast motion to have a upper bound rotation calibration");
            Console.WriteLine("Press enter to start");
            Console.ReadLine();

            try
            {
                var task = GetData(UserCalibrationTask.Calibrating.FastGyro);
                _fastGyro = task.GetDesired();
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

        bool GetSlowGyro()
        {
            Console.WriteLine("Rotate your wrist in a slow motion to have a lower bound rotation calibration");
            Console.WriteLine("Press enter to start");
            Console.ReadLine();

            try
            {
                var task = GetData(UserCalibrationTask.Calibrating.SlowGyro);
                _slowGyro = task.GetDesired();
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
            return GetData(currentlyCalibrating, 5000);
        }

        UserCalibrationTask GetData(UserCalibrationTask.Calibrating currentlyCalibrating, int waitTime) 
        {
            var task = new UserCalibrationTask(currentlyCalibrating);
            var manager = new MyomiTaskManager((1000 / 50), task);
            var taskThread = new Thread(manager.Run);
            Console.WriteLine("Starting new Manager task");

            //we are giving the users 5 seconds to calibrate their arm motion
            taskThread.Start();
            CommonOperations.Sleep(waitTime);

            manager.StopExecution = true;
            return task;
        }

        private bool AnalyzeResults()
        {
            if (_slowAccel >= _fastAccel) 
            {
                Console.WriteLine("Minimum acceleration cannot be greater than maximum acceleration, please recalibrate");
                return false;
            }
            if (_slowGyro >= _fastGyro) 
            {
                Console.WriteLine("Minimum gyro rotation cannot be greater than maximum gyro rotation, please recalibrate");
                return false;
            }
            Console.WriteLine("Current calibration values");
            Console.WriteLine("Maximum Acceleration: {0} m/s^2", _fastAccel);
            Console.WriteLine("Minimum Acceleration: {0} m/s^2", _slowAccel);
            Console.WriteLine("Maximum Gyro rotation: {0} m/s", _fastGyro);
            Console.WriteLine("Minimum Gyro rotation: {0} m/s", _slowGyro);
            Console.WriteLine("Do you wish to keep these values? (Type 'yes' or 'no')");
            string option = Console.ReadLine();
            while (option != "yes" && option != "no") 
            {
                Console.WriteLine("Please enter a valid choice (Type 'yes' or 'no')");
                option = Console.ReadLine();
            }
            if (option == "yes") 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Dictionary<string, string> GetCollected() 
        {
            var toReturn = new Dictionary<string, string>();
            toReturn.Add("FastAccel", _fastAccel.ToString());
            toReturn.Add("SlowAccel", _slowAccel.ToString());
            toReturn.Add("FastGyro", _fastGyro.ToString());
            toReturn.Add("SlowGyro", _slowGyro.ToString());
            toReturn.Add("Arm", _arm.ToString());
            return toReturn;
        }
    }
}
