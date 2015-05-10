using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Config;
using Myomi.Wrapper;
using Myomi.Data;
using System.IO;

namespace Myomi
{
    //this class will contain all the necessary settings and everything of the program
    //basically, it is the global variable holder of this program
    internal class Context
    {
        private static Context _context;
        public PointsConfig Points { get; set; }
        public UserCalibrationConfig UserCalibration { get; set; }
        public MyomiProfile CurrentProfile { get; set; }
        public Dictionary<string, Guid> Profiles { get; private set; }
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
                    _context.InitalizeConfig();
                }
                return _context;
            }
        }

        private Context() 
        {
            GlobalTaskHalt = false;
            DefaultFrequency = (1000 / 20); //the default frequency for this program is 20hz
        }

        public bool AddNewProfile(MyomiProfile toAdd) 
        {
            //first, check to see if there are duplicated names
            if (Profiles.Any(profile => profile.Key == toAdd.Name))
            {
                return false;
            }
            this.Profiles.Add(toAdd.Name, toAdd.ID);
            //after we add it, we would like to set the focus to this profile
            this.CurrentProfile = toAdd;
            return true;
        }

        public bool LoadProfile(string name) 
        {
            if (Profiles.ContainsKey(name)) 
            {
                var profile = new MyomiProfile(Profiles[name]);
                this.CurrentProfile = profile;
                return true;
            }
            return false;
        }

        //these classes references Context, so we need to move it outside
        private void InitalizeConfig()
        {
            Hub = new MyomiHub();
            //should be run on a separate thread since getting the myo to set up is a whole different task
            //but disables the user from continuing if a myo is not detected in MyomiHub
            Points = new PointsConfig();
            //this should be initiated after a myo is found
            UserCalibration = new UserCalibrationConfig();
            InitalizeProfiles();
        }

        private void InitalizeProfiles() 
        {
            var profileFolders = FileHelper.GetAllFolders("Profiles");
            //our profile folder will have its name as a guid... because we rock it this way
            foreach (var profileFolder in profileFolders) 
            {
                try
                {
                    var metadata = FileHelper.GetValuesFromFile(Path.Combine("Profiles", profileFolder, "metadata.txt"));
                    var name = metadata["name"];
                    Profiles.Add(name, new Guid(profileFolder));
                }
                catch (KeyNotFoundException e) 
                {
                    Console.WriteLine("Profile {0}'s metadata is corrupt, please try the fix option in menu. Skipping file for now", profileFolder);
                    continue;
                }
                catch (Exception e) 
                {
                    Console.WriteLine("An error has occured when attempting to add a profile");
                    Console.WriteLine(e.ToString());
                    continue;
                }
            }
        }

        public static void Initialize()
        {
            if (_context != null)
                return;

            _context = new Context();
            _context.InitalizeConfig();
        }
    }
}
