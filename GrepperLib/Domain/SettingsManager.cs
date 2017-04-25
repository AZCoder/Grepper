using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using GrepperLib.Model;

namespace GrepperLib.Domain
{
    public class SettingsManager : ISettings
    {
        protected Settings _settings;

        public SettingsManager()
        {
            _settings = new Settings();
        }

        public bool SaveSettings(Settings settings)
        {
            bool isSaved = false;
            string json = JsonConvert.SerializeObject(settings);
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\settings.json";
            try
            {
                File.WriteAllText(path, json);
                isSaved = true;
            }
            catch (Exception)
            {
                isSaved = false;
            }

            return isSaved;
        }

        public bool DeleteExtension(string extension)
        {
            bool isDeleted = false;


            return isDeleted;
        }

        public IList<string> LoadExtensions()
        {

            throw new NotImplementedException();
        }

        public Settings LoadSettings()
        {

            return _settings;
        }
    }
}
