using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myomi.Data
{
    internal class MyomiGestureOptions
    {

        public bool PoseEnabled { get; set; }
        public bool AccelEnabled { get; set; }
        public bool GyroEnabled { get; set; }
        public bool OrienEnabled { get; set; }
        public bool AccelNormOnly { get; set; }
        public bool GyroNormOnly { get; set; }
        //half mode would be that instead of divding it up into 4 zones, we divide it up into 2
        //one side will be 0, the other will be just 2 in value
        public bool OrienHalfMode { get; set; }
        public bool IgnoreFrameCounts { get; set; }

        public MyomiGestureOptions()
        {
            EnableAll();
        }

        public MyomiGestureOptions(string optionsString)
        {
            //the format we have is the standard value:value:value:... format
            var options = optionsString.Split(':').Select(s => Int32.Parse(s)).ToArray(); //Hell yeah lambdas!
            this.PoseEnabled = options[0] == 1 ? true : false;
            this.AccelEnabled = options[1] == 1 ? true : false;
            this.GyroEnabled = options[2] == 1 ? true : false;
            this.OrienEnabled = options[3] == 1 ? true : false;
            this.AccelNormOnly = options[4] == 1 ? true : false;
            this.GyroNormOnly = options[5] == 1 ? true : false;
            this.OrienHalfMode = options[6] == 1 ? true : false;
            this.IgnoreFrameCounts = options[7] == 1 ? true : false;
        }

        public void EnableAll()
        {
            this.PoseEnabled = true;
            this.AccelEnabled = true;
            this.GyroEnabled = true;
            this.OrienEnabled = true;
            this.AccelNormOnly = true;
            this.GyroNormOnly = true;
            this.OrienHalfMode = true;
            this.IgnoreFrameCounts = true;
        }

        public void DisableAll()
        {
            //shouldn't really be called and left there... at least 1 setting should be enabled or else it's a really wierd
            //profile
            this.PoseEnabled = false;
            this.AccelEnabled = false;
            this.GyroEnabled = false;
            this.OrienEnabled = false;
            this.AccelNormOnly = false;
            this.GyroNormOnly = false;
            this.OrienHalfMode = false;
            this.IgnoreFrameCounts = false;
        }

        public string ToFileString()
        {
            string returnString = "";
            returnString = AppendStringFromBool(returnString, this.PoseEnabled);
            returnString = AppendStringFromBool(returnString, this.AccelEnabled);
            returnString = AppendStringFromBool(returnString, this.GyroEnabled);
            returnString = AppendStringFromBool(returnString, this.OrienEnabled);
            returnString = AppendStringFromBool(returnString, this.AccelNormOnly);
            returnString = AppendStringFromBool(returnString, this.GyroNormOnly);
            returnString = AppendStringFromBool(returnString, this.OrienHalfMode);
            returnString = AppendStringFromBool(returnString, this.IgnoreFrameCounts);
            returnString.Remove(returnString.Length - 1);
            return returnString;
        }

        private string AppendStringFromBool(string toAppend, bool toEvalulate)
        {
            if (toEvalulate)
            {
                return toAppend + "1:";
            }
            else
            {
                return toAppend + "0:";
            }
        }
    }
}
