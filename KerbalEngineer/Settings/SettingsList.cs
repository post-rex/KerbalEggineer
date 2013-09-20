﻿// Name:    Kerbal Engineer Redux
// Author:  CYBUTEK
// License: Attribution-NonCommercial-ShareAlike 3.0 Unported

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KerbalEngineer.Settings
{
    [Serializable]
    public class SettingsList
    {
        public List<Setting> Settings = new List<Setting>();

        /// <summary>
        /// Add a setting into this settings list.
        /// </summary>
        public void AddSetting(string name, object value)
        {
            foreach (Setting setting in Settings)
            {
                if (setting.Name == name)
                {
                    setting.Value = value;
                    return;
                }
            }

            Settings.Add(new Setting(name, value));
        }

        /// <summary>
        /// Gets a setting value from this settings list, or returns the default value.
        /// </summary>
        public object GetSetting(string name, object defaultValue)
        {
            foreach (Setting setting in Settings)
            {
                if (setting.Name == name)
                    return setting.Value;
            }

            AddSetting(name, defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Creates a settings list from an existing file, or returns a blank settings list object.
        /// </summary>
        public static SettingsList CreateFromFile(string filename)
        {
            if (File.Exists(filename))
            {
                try
                {
                    return new BinaryFormatter().Deserialize(File.OpenRead(filename)) as SettingsList;
                }
                catch { throw new Exception("Could not load settings from file."); }
            }

            return new SettingsList();
        }

        /// <summary>
        /// Saves a settings list to a file.
        /// </summary>
        public static void SaveToFile(string filename, SettingsList settingList)
        {
            if (!Directory.Exists(new FileInfo(filename).DirectoryName))
                Directory.CreateDirectory(new FileInfo(filename).DirectoryName);

            try
            {
                new BinaryFormatter().Serialize(File.OpenWrite(filename), settingList);
            }
            catch { throw new Exception("Could not save settings to file."); }
        }
    }
}
