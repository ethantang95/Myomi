using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Data;

namespace Myomi.Task.NonHandleTasks
{
    class ProfileManagingTask
    {
        private MyomiProfile _profile;

        public ProfileManagingTask(MyomiProfile profile)
        {
            this._profile = profile;
        }

        public void Run()
        {
            int decision = -1;
            while (decision != 0)
            {
                Console.WriteLine("Welcome to profile management, currently selected profile is {0}", this._profile.Name);
                Console.WriteLine("Please select one of the options below or type 0 to quit");
                Console.WriteLine("Type '1' to change the settings of the profile");
                Console.WriteLine("Type '2' to change the name of the profile");
                Console.WriteLine("Type '0' to exit");
                var optionString = Console.ReadLine();
                decision = CommonOperations.GetOptionValue(optionString, 1, 2);
                if (decision == -1)
                {
                    Console.WriteLine("The input was not valid");
                }
                else
                {
                    DecisionLadder(decision);
                }
            }
        }

        private void DecisionLadder(int decision)
        {
            switch (decision)
            {
                case 0: return;
                case 1: ChangeSetting();
                    break;
                case 2: ChangeName();
                    break;
                default: return;
            }
            this._profile.SaveProfile();
        }

        private void ChangeSetting()
        {
            Console.WriteLine("Please type in the value for the settings");
            Console.WriteLine("Would you like to enable mouse control for this profile");
            this._profile.Options.EnableMouseControl = CommonOperations.GetYesOrNo();
            Console.WriteLine("Would you like to enable fast activation for this profile");
            this._profile.Options.FastActivation = CommonOperations.GetYesOrNo();
            Console.WriteLine("Saving your settings");
        }

        private void ChangeName()
        {
            Console.WriteLine("Please type in the name for this profile");
            var name = Console.ReadLine();
            if (Context.Instance.Profiles.Any(profile => profile.Key == name))
            {
                Console.WriteLine("This name has already been taken");
            }
            else
            {
                this._profile.Name = name;
            }
        }
    }
}
