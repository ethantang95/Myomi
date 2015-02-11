using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.FromFiles;
using Myomi.Instances;

namespace Myomi
{
    //this class will contain all the necessary settings and everything of the program
    //basically, it is the global variable holder of this program
    class Context
    {
        private static Context _context;
        public PointsConfig Points { get; set; }
        public MyomiHub Hub { get; set; }
        public MyomiMyo Myo { get; set; }
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
            Points.Initialize();
            Hub.Initialize();
            //should be run on a separate thread since getting the myo to set up is a whole different task
            //but disables the user from continuing
            Myo.Initialize();

        }

        public static void Initialize()
        {
            if (_context != null)
                return;

            _context = new Context();
        }
    }
}
