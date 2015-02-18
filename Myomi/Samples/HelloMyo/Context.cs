using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Config;
using Myomi.Wrapper;

namespace Myomi
{
    //this class will contain all the necessary settings and everything of the program
    //basically, it is the global variable holder of this program
    class Context
    {
        private static Context _context;
        public PointsConfig Points { get; set; }
        public UserCalibrationConfig UserCalibration { get; set; }
        public MyomiHub Hub { get; set; }
        public double DefaultFrequency { get; set; }
        //the global task halt shall only be used whenever we want to immeditately stop execution anywhere
        public bool GlobalTaskHalt { get; set; }

        public static Context Instance
        {
            get
            {
                //theoretically, it should not be called in this program since
                //we have initialize to do this
                if (_context == null)
                {
                    _context = new Context();
                }
                return _context;
            }
        }

        private Context() 
        {
            GlobalTaskHalt = false;
            DefaultFrequency = (1000 / 50); //the default frequency for this program is 20hz
            Hub = new MyomiHub();
            //should be run on a separate thread since getting the myo to set up is a whole different task
            //but disables the user from continuing if a myo is not detected in MyomiHub
            Points = new PointsConfig();
            //this shoudl be initiated after a myo is found
            UserCalibration = new UserCalibrationConfig();
        }

        public static void Initialize()
        {
            if (_context != null)
                return;

            _context = new Context();
        }
    }
}
