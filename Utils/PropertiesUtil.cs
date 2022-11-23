using KMDVFramework.Config;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KMDVFramework.Utils
{
    public class PropertiesUtil : Constants
    {
        private Dictionary<string, string> Property = new Dictionary<string, string>();

        public PropertiesUtil(string PropertiesPath)
        {
            string FileAsString = File.ReadAllText(PropertiesPath);
            string[] LinesSplit = FileAsString.Split("\n");
            foreach(string line in LinesSplit)
            {
                if(line.Contains("=") && !line.Contains("#"))
                {
                    string[] LineSplit = line.Split("=", 2);
                    Property.Add(LineSplit[0].Trim(), LineSplit[1].Trim());
                }
                
            }
        }

        public string GetProperty(string Key)
        {
            return Property[Key];
        }

       public Dictionary<string, string> GetDictionary()
        {
            return Property;
        }

        public Dictionary<string, string>.KeyCollection GetPropertyKeys()
        {
            return Property.Keys;
        }

        public Dictionary<string, string>.ValueCollection GetPropertyValues()
        {
            return Property.Values;
        }

        public bool PropertyContainsKey(string Key)
        {
            return Property.ContainsKey(Key);
        }

        public bool PropertyContainsValue(string Value)
        {
            return Property.ContainsValue(Value);
        }
    }
}
