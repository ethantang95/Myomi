using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using WindowsInput;
using WindowsInput.Native;

namespace Myomi.Data
{
    class MyomiGestureAction
    {
        private VirtualKeyCode _keyToPress;
        private int _delay;
        private bool _isMouse;
        private InputSimulator _sim;

        public MyomiGestureAction(VirtualKeyCode keyToPress, int delay)
        {
            this._keyToPress = keyToPress;
            this._delay = delay;
            this._sim = new InputSimulator();
            this._isMouse = IsMouse(keyToPress);
        }

        public MyomiGestureAction(string actionString)
        {
            // TODO: Complete member initialization
            var actionValues = actionString.Split(':');
            this._keyToPress = (VirtualKeyCode) Enum.Parse(typeof (VirtualKeyCode), actionValues[0]);
            this._delay = Int32.Parse(actionValues[1]);
        }

        public bool ExecuteAction()
        {
            try
            {
                if (this._isMouse)
                {
                    switch (this._keyToPress)
                    {
                        case VirtualKeyCode.LBUTTON:
                            return LeftMouseClick();
                        case VirtualKeyCode.RBUTTON:
                            return RightMouseClick();
                        default:
                            Console.WriteLine("Illegal mouse click detected");
                            return false;
                    }
                }
                else
                {
                    var keyboardSim = new KeyboardSimulator(_sim);
                    keyboardSim.KeyDown(this._keyToPress);
                    var delayTimer = new Timer(_delay);

                    delayTimer.Elapsed += (sender, args) => 
                    { 
                        keyboardSim.KeyUp(this._keyToPress);
                        delayTimer.Enabled = false;
                    };
                    delayTimer.Enabled = true;
                    return true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Action did not execute due to exception");
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public string ToFileString()
        {
            return this._keyToPress.ToString() + ":" + this._delay;
        }

        private bool LeftMouseClick()
        {
            var mouseSim = new MouseSimulator(_sim);
            mouseSim.LeftButtonDown();
            var delayTimer = new Timer(_delay);

            delayTimer.Elapsed += (sender, args) =>
            {
                mouseSim.LeftButtonUp();
                delayTimer.Enabled = false;
            };
            delayTimer.Enabled = true;
            return true;
        }

        private bool RightMouseClick()
        {
            var mouseSim = new MouseSimulator(_sim);
            mouseSim.RightButtonDown();
            var delayTimer = new Timer(_delay);

            delayTimer.Elapsed += (sender, args) =>
            {
                mouseSim.RightButtonUp();
                delayTimer.Enabled = false;
            };
            delayTimer.Enabled = true;
            return true;
        }

        private static bool IsMouse(VirtualKeyCode toCheck)
        {
            return (toCheck == VirtualKeyCode.LBUTTON || toCheck == VirtualKeyCode.RBUTTON || toCheck == VirtualKeyCode.MBUTTON);
        }
    }
}
