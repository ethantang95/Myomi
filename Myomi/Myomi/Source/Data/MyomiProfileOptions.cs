using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myomi.Data
{
    internal class MyomiProfileOptions
    {
        public bool EnableMouseControl { get; set; }
        //fast activation is used for activating a gesture AS soon as one is isolated. This means that if only 1 gesture
        //is detected, it will be activated
        public bool FastActivation { get; set; }

        public MyomiProfileOptions()
        {
            EnableAll();
        }

        public void EnableAll() 
        {
            this.EnableMouseControl = true;
            this.FastActivation = true;
        }

        public void DisableAll() 
        {
            this.EnableMouseControl = false;
            this.FastActivation = false;
        }

        public Dictionary<string, string> ToFile()
        {
            var toReturn = new Dictionary<string, string>();
            toReturn.Add("EnableMouseControl", this.EnableMouseControl.ToString());
            toReturn.Add("FastActivation", this.FastActivation.ToString());
            return toReturn;
        }
    }
}
