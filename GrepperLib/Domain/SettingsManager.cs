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
        protected readonly string _path;

        public SettingsManager()
        {
            _settings = new Settings();
            _path = AppDomain.CurrentDomain.BaseDirectory + @"\settings.json";
        }

        public bool SaveSettings(Settings settings)
        {
            bool isSaved = false;
            // get extensions and search terms before updating
            _settings = LoadSettings();
            AddSearchTerm(_settings, settings.SearchTerm);
            settings.SavedSearchTerms = _settings.SavedSearchTerms;
            AddExtension(_settings, settings.LastExtension);
            settings.SavedExtensions = _settings.SavedExtensions;

            string json = JsonConvert.SerializeObject(settings);
            try
            {
                File.WriteAllText(_path, json);
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
            extension = extension.Trim();
            var extensions = GetExtensions();
            if (!extensions.Contains(extension))
                return isDeleted;

            if (extensions.Remove(extension))
            {
                isDeleted = SaveExtensions(extensions);
            }

            return isDeleted;
        }

        public IList<string> GetExtensions()
        {
            _settings = LoadSettings();
            return _settings.SavedExtensions;
        }

        public Settings LoadSettings()
        {
            try
            {
                var json = File.ReadAllText(_path);
                _settings = JsonConvert.DeserializeObject<Settings>(json);
            }
            catch (Exception ex)
            {
                Utility.Message.Add("Unable to load settings: " + ex.Message);
            }

            return _settings;
        }

        private void AddExtension(Settings settings, string extension)
        {
            if (settings == null || string.IsNullOrEmpty(extension))
                return;

            if (settings.SavedExtensions == null)
                settings.SavedExtensions = new List<string>();

            extension = extension.Trim();
            if (!settings.SavedExtensions.Contains(extension))
                settings.SavedExtensions.Add(extension);
        }

        private void AddSearchTerm(Settings settings, string term)
        {
            if (settings == null || string.IsNullOrEmpty(term))
                return;

            if (settings.SavedSearchTerms == null)
                settings.SavedSearchTerms = new List<string>();

            term = term.Trim();
            if (!settings.SavedSearchTerms.Contains(term))
                settings.SavedSearchTerms.Add(term);
        }

        public bool SaveExtensions(IList<string> extensions)
        {
            // note that this will OVERRIDE all existing extensions
            bool isSaved = false;
            if (extensions == null)
                return isSaved;

            _settings = LoadSettings();
            extensions = RemoveDuplicates(extensions);
            _settings.SavedExtensions = extensions;
            string json = JsonConvert.SerializeObject(_settings);
            try
            {
                File.WriteAllText(_path, json);
                isSaved = true;
            }
            catch (Exception)
            {
                isSaved = false;
            }

            return isSaved;
        }

        public bool SaveSearches(IList<string> searches)
        {
            // note that this will OVERRIDE all existing search terms
            bool isSaved = false;
            if (searches == null)
                return isSaved;

            _settings = LoadSettings();
            searches = RemoveDuplicates(searches);
            _settings.SavedSearchTerms = searches;
            string json = JsonConvert.SerializeObject(_settings);
            try
            {
                File.WriteAllText(_path, json);
                isSaved = true;
            }
            catch (Exception)
            {
                isSaved = false;
            }

            return isSaved;
        }

        private IList<string> RemoveDuplicates(IList<string> listToClean)
        {
            List<string> cleanList = new List<string>();
            foreach (var item in listToClean)
            {
                if (!string.IsNullOrEmpty(item) && !cleanList.Contains(item))
                    cleanList.Add(item);
            }

            return cleanList;
        }
    }
}
