using Myomi.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Myomi.Data
{
    //this will contain the current profile which will have all the gestures. Here, we will also create the profile
    internal class MyomiProfile
    {
        
        int _gestureCount;

        public string Name { get; set; }
        public List<MyomiGesture> Gestures { get; private set; }
        public MyomiProfileOptions Options { get; set; }
        public Guid ID { get; set; }

        //called when a new profile is being created
        public MyomiProfile(string name) 
        {
            this.Name = name;
            this.ID = Guid.NewGuid();
            this.Options = new MyomiProfileOptions();
            this.SaveProfile();
        }

        //given the profileID, we will know which profile we should load
        public MyomiProfile(Guid profileID) 
        {
            this.ID = profileID;
            PopulateGestures();
            PopulateOptions();
            GetMetadata();
        }

        public void DeleteGesture(string name)
        {
            MyomiGesture toRemove = null;
            //first, we need to find that gesture
            foreach (var gesture in this.Gestures) 
            {
                if (gesture.Name == name) 
                {
                    gesture.ID = Guid.Empty;
                    toRemove = gesture;
                }
            }
            if (toRemove != null)
            {
                Gestures.Remove(toRemove);
            }
        }

        public void AddGesture(MyomiGesture gesture) 
        {
            this.Gestures.Add(gesture);
            this.SaveProfile();
        }

        public void SaveProfile()
        {
            //First, we need to make the data into a format that can be written
            var optionsToWrite = this.Options.ToFile();
            FileHelper.WriteValuesToFile(optionsToWrite, Path.Combine("Profiles", this.ID.ToString(), "profile.config"));
            //then the metadata
            var metaToWrite = MetadataToFile();
            FileHelper.WriteValuesToFile(metaToWrite, Path.Combine("Profiles", this.ID.ToString(), "metadata.txt"));

            foreach (var gesture in Gestures)
            {
                gesture.SaveGesture(this.ID);
            }
        }

        private void PopulateGestures() 
        {
            //we will get another list of amazing guids which each contains a gesture, then we can let the gesture creator worry about that later
            //we are going to use the extention mygest for myomi gesture files
            var allGestures = FileHelper.GetAllFiles(Path.Combine("Profiles", this.ID.ToString()), ".mygest");
            //first we will populate the gestures
            foreach (var gesturePath in allGestures)
            {
                var gesture = new MyomiGesture(new Guid(gesturePath), this.ID);
                //this one is broken, we will skip this gesture
                if (gesture.ID == Guid.Empty)
                {
                    continue;
                }
                Gestures.Add(gesture);
                this._gestureCount++;
            }
        }

        private void PopulateOptions() 
        {
            var options = FileHelper.GetValuesFromFile(Path.Combine("Profiles", this.ID.ToString(), "profile.config"));
            this.Options = new MyomiProfileOptions();
            try
            {
                this.Options.EnableMouseControl = Boolean.Parse(options["EnableMouseControl"]);
                this.Options.FastActivation = Boolean.Parse(options["FastActivation"]);
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine("The profile.config file is corrupt for profile {0}", this.ID.ToString());
                Console.WriteLine("Setting this file to default values");
            }
            catch (Exception e) 
            {
                Console.WriteLine("Something went wrong when reading the profile.config file for profile {0}", this.ID.ToString());
                throw;
            }
        }

        private void GetMetadata() 
        {
            //not even sure what should be in this... all I can think of right now is just name
            var metadata = FileHelper.GetValuesFromFile(Path.Combine("Profiles", this.ID.ToString(), "metadata.txt"));
            try
            {
                this.Name = metadata["name"];
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine("The metadata file is corrupt for profile {0}", this.ID.ToString());
                Console.WriteLine("Setting it to a default value");
                this.Name = "Profile-" + this.ID.ToString();
            }
            catch (Exception e) 
            {
                Console.WriteLine("Something went wrong when reading the metadata file for profile {0}", this.ID.ToString());
                throw;
            }
        }

        private Dictionary<string, string> MetadataToFile() 
        {
            var toReturn = new Dictionary<string, string>();
            toReturn.Add("Name", this.Name);
            return toReturn;
        }
    }
}
