using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsInput.Native;

namespace Myomi
{
    class KeyToKeyBind
    {
        public static VirtualKeyCode ParseKey(string key)
        {
            //for the letters
            if (CheckKey(key, "a"))
            {
                return VirtualKeyCode.VK_A;
            }
            if (CheckKey(key, "b"))
            {
                return VirtualKeyCode.VK_B;
            }
            if (CheckKey(key, "c"))
            {
                return VirtualKeyCode.VK_C;
            }
            if (CheckKey(key, "d"))
            {
                return VirtualKeyCode.VK_D;
            }
            if (CheckKey(key, "e"))
            {
                return VirtualKeyCode.VK_E;
            }
            if (CheckKey(key, "f"))
            {
                return VirtualKeyCode.VK_F;
            }
            if (CheckKey(key, "g"))
            {
                return VirtualKeyCode.VK_G;
            }
            if (CheckKey(key, "h"))
            {
                return VirtualKeyCode.VK_H;
            }
            if (CheckKey(key, "i"))
            {
                return VirtualKeyCode.VK_I;
            }
            if (CheckKey(key, "j"))
            {
                return VirtualKeyCode.VK_J;
            }
            if (CheckKey(key, "k"))
            {
                return VirtualKeyCode.VK_K;
            }
            if (CheckKey(key, "l"))
            {
                return VirtualKeyCode.VK_L;
            }
            if (CheckKey(key, "m"))
            {
                return VirtualKeyCode.VK_M;
            }
            if (CheckKey(key, "n"))
            {
                return VirtualKeyCode.VK_N;
            }
            if (CheckKey(key, "o"))
            {
                return VirtualKeyCode.VK_O;
            }
            if (CheckKey(key, "p"))
            {
                return VirtualKeyCode.VK_P;
            }
            if (CheckKey(key, "q"))
            {
                return VirtualKeyCode.VK_Q;
            }
            if (CheckKey(key, "r"))
            {
                return VirtualKeyCode.VK_R;
            }
            if (CheckKey(key, "s"))
            {
                return VirtualKeyCode.VK_S;
            }
            if (CheckKey(key, "t"))
            {
                return VirtualKeyCode.VK_T;
            }
            if (CheckKey(key, "u"))
            {
                return VirtualKeyCode.VK_U;
            }
            if (CheckKey(key, "v"))
            {
                return VirtualKeyCode.VK_V;
            }
            if (CheckKey(key, "w"))
            {
                return VirtualKeyCode.VK_W;
            }
            if (CheckKey(key, "x"))
            {
                return VirtualKeyCode.VK_X;
            }
            if (CheckKey(key, "y"))
            {
                return VirtualKeyCode.VK_Y;
            }
            if (CheckKey(key, "z"))
            {
                return VirtualKeyCode.VK_Z;
            }

            //for the numbers now
            if (CheckKey(key, "0"))
            {
                return VirtualKeyCode.VK_0;
            }
            if (CheckKey(key, "1"))
            {
                return VirtualKeyCode.VK_1;
            }
            if (CheckKey(key, "2"))
            {
                return VirtualKeyCode.VK_2;
            }
            if (CheckKey(key, "3"))
            {
                return VirtualKeyCode.VK_3;
            }
            if (CheckKey(key, "4"))
            {
                return VirtualKeyCode.VK_4;
            }
            if (CheckKey(key, "5"))
            {
                return VirtualKeyCode.VK_5;
            }
            if (CheckKey(key, "6"))
            {
                return VirtualKeyCode.VK_6;
            }
            if (CheckKey(key, "7"))
            {
                return VirtualKeyCode.VK_7;
            }
            if (CheckKey(key, "8"))
            {
                return VirtualKeyCode.VK_8;
            }
            if (CheckKey(key, "9"))
            {
                return VirtualKeyCode.VK_9;
            }
            
            //control, shift, enter, tab
            if (CheckKey(key, "ctrl", "control"))
            {
                return VirtualKeyCode.CONTROL;
            }
            if (CheckKey(key, "enter", "return"))
            {
                return VirtualKeyCode.RETURN;
            }
            if (CheckKey(key, "shift"))
            {
                return VirtualKeyCode.SHIFT;
            }
            if (CheckKey(key, "tab"))
            {
                return VirtualKeyCode.TAB;
            }

            //arrow keys
            if (CheckKey(key, "up", "uparrow", "upkey"))
            {
                return VirtualKeyCode.UP;
            }
            if (CheckKey(key, "down", "downarrow", "downkey"))
            {
                return VirtualKeyCode.DOWN;
            }
            if (CheckKey(key, "left", "leftarrow", "leftkey"))
            {
                return VirtualKeyCode.LEFT;
            }
            if (CheckKey(key, "right", "rightarrow", "rightkey"))
            {
                return VirtualKeyCode.RIGHT;
            }

            //f keys
            if (CheckKey(key, "f1"))
            {
                return VirtualKeyCode.F1;
            }
            if (CheckKey(key, "f2"))
            {
                return VirtualKeyCode.F2;
            }
            if (CheckKey(key, "f3"))
            {
                return VirtualKeyCode.F3;
            }
            if (CheckKey(key, "f4"))
            {
                return VirtualKeyCode.F4;
            }
            if (CheckKey(key, "f5"))
            {
                return VirtualKeyCode.F5;
            }
            if (CheckKey(key, "f6"))
            {
                return VirtualKeyCode.F6;
            }
            if (CheckKey(key, "f7"))
            {
                return VirtualKeyCode.F7;
            }
            if (CheckKey(key, "f8"))
            {
                return VirtualKeyCode.F8;
            }
            if (CheckKey(key, "f9"))
            {
                return VirtualKeyCode.F9;
            }
            if (CheckKey(key, "f10"))
            {
                return VirtualKeyCode.F10;
            }
            if (CheckKey(key, "f11"))
            {
                return VirtualKeyCode.F11;
            }
            if (CheckKey(key, "f12"))
            {
                return VirtualKeyCode.F12;
            }

            //symbolic keys (I have no idea what these actually are
            if (CheckKey(key, "`"))
            {
                return VirtualKeyCode.OEM_3;
            }
            if (CheckKey(key, "-"))
            {
                return VirtualKeyCode.OEM_MINUS;
            }
            if (CheckKey(key, "="))
            {
                return VirtualKeyCode.OEM_PLUS;
            }
            if (CheckKey(key, "["))
            {
                return VirtualKeyCode.OEM_4;
            }
            if (CheckKey(key, "]"))
            {
                return VirtualKeyCode.OEM_6;
            }
            if (CheckKey(key, "\\"))
            {
                return VirtualKeyCode.OEM_5;
            }
            if (CheckKey(key, ";"))
            {
                return VirtualKeyCode.OEM_1;
            }
            if (CheckKey(key, "'"))
            {
                return VirtualKeyCode.OEM_7;
            }
            if (CheckKey(key, ","))
            {
                return VirtualKeyCode.OEM_COMMA;
            }
            if (CheckKey(key, "."))
            {
                return VirtualKeyCode.OEM_PERIOD;
            }
            if (CheckKey(key, "-"))
            {
                return VirtualKeyCode.OEM_2;
            }

            //the weird buttons
            if (CheckKey(key, "insert", "ins"))
            {
                return VirtualKeyCode.INSERT;
            }
            if (CheckKey(key, "delete", "del"))
            {
                return VirtualKeyCode.DELETE;
            }
            if (CheckKey(key, "home", "hm"))
            {
                return VirtualKeyCode.HOME;
            }
            if (CheckKey(key, "end"))
            {
                return VirtualKeyCode.END;
            }
            if (CheckKey(key, "pagedown", "pgdown", "pagedn", "pgdn"))
            {
                return VirtualKeyCode.NEXT;
            }
            if (CheckKey(key, "pageup", "pgup"))
            {
                return VirtualKeyCode.PRIOR;
            }
            if (CheckKey(key, "back", "backspace", "bkspace", "bk"))
            {
                return VirtualKeyCode.BACK;
            }
            if (CheckKey(key, "esc", "escape"))
            {
                return VirtualKeyCode.ESCAPE;
            }
            if (CheckKey(key, "space", "spacebar"))
            {
                return VirtualKeyCode.SPACE;
            }

            //mouse buttons
            if (CheckKey(key, "leftclick", "leftmouseclick", "leftmouse"))
            {
                return VirtualKeyCode.LBUTTON;
            }
            if (CheckKey(key, "rightclick", "rightmouseclick", "rightmouse"))
            {
                return VirtualKeyCode.RBUTTON;
            }

            //and if the person entered something that's not valid
            return VirtualKeyCode.NONAME;
        }

        private static bool CheckKey(string toCheck, params string[] toMatch)
        {
            return toMatch.Any(toMatchEntry => string.Equals(toCheck, toMatchEntry, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
