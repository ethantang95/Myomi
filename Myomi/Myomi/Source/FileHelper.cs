﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

//any general methods or tasks that have to deal with files will be here, this includes restoring, getting, and writing to files, also folders, it will also help with folders
namespace Myomi
{
    internal static class FileHelper
    {
        //the default save location should be the user's document folder.... or maybe program data but I like docs better
        static string GetProgramSaveLocation(string fileName)
        {
            return Path.Combine("C:", "ProgramData", "Myomi", fileName);
        }
        static string GetOptionsSaveLocation(string filename) 
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Myomi", filename);
        }
        //get files that have the signature or design of name:value
        public static Dictionary<string, string> GetValuesFromFile(string fileName)
        {
            var toReturn = new Dictionary<string, string>();
            try
            {
                using (var reader = new StreamReader(GetProgramSaveLocation(fileName)))
                {
                    string line;
                    while (((line = reader.ReadLine()) != null) && (line != String.Empty))
                    {
                        var configLine = line.Split(':');
                        toReturn.Add(configLine[0], configLine[1]);
                    }
                }
                return toReturn;
            }
            catch (Exception e)
            {
                if (e is IOException || e is IndexOutOfRangeException)
                {
                    Console.WriteLine("The {0} file is corrupt.", fileName);
                    return null;
                }
                else
                {
                    Console.WriteLine("Something went wrong when reading from file {0}", fileName);
                    Console.WriteLine(e.ToString());
                    throw e;
                }
            }
        }

        public static List<string> GetContentFromFile(string fileName) 
        {
            var toReturn = new List<string>();
            try
            {
                using (var reader = new StreamReader(GetProgramSaveLocation(fileName)))
                {
                    string line;
                    while (((line = reader.ReadLine()) != null))
                    {
                        toReturn.Add(line);
                    }
                }
                return toReturn;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("The file {0} was not found", fileName);
                return null;
            }
            catch (Exception e) 
            {
                Console.WriteLine("Something went wrong when trying to read file {0}", fileName);
                Console.WriteLine(e.ToString());
                throw e;
            }
        }
        public static bool WriteValuesToFile(Dictionary<string, string> toWrite, string fileName)
        {
            try
            {
                using (var writer = new StreamWriter(GetProgramSaveLocation(fileName))) 
                {
                    foreach (var entry in toWrite) 
                    {
                        writer.WriteLine("{0}:{1}", entry.Key, entry.Value);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong while writing {0} file", fileName);
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public static bool WriteToFile(List<string> toWrite, string fileName) 
        {
            try
            {
                using (var writer = new StreamWriter(GetProgramSaveLocation(fileName)))
                {
                    foreach (var entry in toWrite)
                    {
                        writer.WriteLine(entry);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong while writing {0} file", fileName);
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public static List<string> GetAllFolders(string folderName) 
        {
            //here, we try to get a list of the files, we will have an inner try and catch loop for any problems inside the profiles directory
            try 
            {
                var toReturn = new List<string>();
                var folders = Directory.GetDirectories(GetProgramSaveLocation(folderName));
                foreach (var folderPath in folders) 
                {
                    //we are returning the folder name only, not the path... this is like a hack to get the folder name only
                    var folder = folderPath.Split(Path.DirectorySeparatorChar).Last();
                    toReturn.Add(folder);
                }
                return toReturn;
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Directory was not created, attempting to create the directory");
                Directory.CreateDirectory(GetProgramSaveLocation("Profiles"));
                return new List<string>();
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem getting the folders at {0}", folderName);
                Console.WriteLine(e.ToString());
                throw e;
            }
        }

        public static List<string> GetAllFiles(string folderName) 
        {
            return GetAllFiles(folderName, string.Empty);
        }

        public static List<string> GetAllFiles(string folderName, string extension)
        {
            //here, we try to get a list of the files, we will have an inner try and catch loop for any problems inside the profiles directory
            try
            {
                var toReturn = new List<string>();
                string[] files;
                if (extension == string.Empty)
                {
                    files = Directory.GetFiles(GetProgramSaveLocation(folderName));
                }
                else 
                {
                    files = Directory.GetFiles(GetProgramSaveLocation(folderName), extension);
                }
                foreach (var filePath in files)
                {
                    //we are returning the folder name only, not the path... this is like a hack to get the folder name only
                    var folder = filePath.Split(Path.DirectorySeparatorChar).Last();
                    toReturn.Add(folder);
                }
                return toReturn;
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Directory was not created, attempting to create the directory");
                Directory.CreateDirectory(GetProgramSaveLocation("Profiles"));
                return new List<string>();
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem getting the files at {0}", folderName);
                Console.WriteLine(e.ToString());
                throw e;
            }
        }
    }
}
