using System;
using MyoNet.Myo;
using Myomi.Data;
using Myomi.Analyzer;
using Myomi.TaskManager;

namespace Myomi
{
	class Program
	{
		static void Main()
		{
            //should be the first line since it basically creates this program and makes it working
            Context.Initialize();
            int decision = 1;
            while(decision != 0)
            {
                Console.WriteLine("Please select one of the options");
                Console.WriteLine("Type '1' to create a new profile");
                Console.WriteLine("Type '2' to create a new gesture in your profile");
                Console.WriteLine("Type '3' to switch to a different profile");
                Console.WriteLine("Type '0' to quit Myomi");
                var decisionChar = Console.ReadLine();
                decision = CommonOperations.GetOptionValue(decisionChar, 1, 3);
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

        private static void DecisionLadder(int decision) 
        {
            switch (decision) 
            {
                case 0: return;
                case 1: CreateProfile();
                    break;
                case 2: CreateGesture();
                    break;
                case 3: SwitchProfile();
                    break;
                default: return;
            }
        }

        private static void CreateProfile()
        {
            Console.WriteLine("What would you like to name your new profile?");
            string profileName = Console.ReadLine();
            var profile = new MyomiProfile(profileName);
            if (Context.Instance.AddNewProfile(profile))
            {
                Console.WriteLine("The new profile has been added, the current profile has been set to it");
            }
            else 
            {
                Console.WriteLine("There is already a profile with the name {0}", profileName);
            }
        }

        private static void CreateGesture() 
        {
            var task = new GestureCreationManager();
            task.Run();
        }

        private static void SwitchProfile() 
        {
            Console.WriteLine("Your current saved profiles");
            foreach (var profile in Context.Instance.Profiles) 
            {
                Console.WriteLine("{0} - ID:{1}", profile.Key, profile.Value.ToString());
            }
            Console.WriteLine("Please type in the name of the profile you want to switch to");
            string profileName = Console.ReadLine();
            if (Context.Instance.LoadProfile(profileName))
            {
                Console.WriteLine("Profile switching success");
            }
            else 
            {
                Console.WriteLine("Profile with name {0} cannot be found", profileName);
            }
        }
	}
}
