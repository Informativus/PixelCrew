using System;
using PixelCrew.Model.Data.Properties;
using UnityEngine;

namespace PixelCrew.Model.Definitions.Localization
{
    public class LocalizationManager
    {
        public static readonly LocalizationManager I;

        private StringPersistentProperty _localeKey = new StringPersistentProperty("en", "localization/current");

        private LocalDef _localeDef;

        public event Action OnLocaleChanged;
        static LocalizationManager()
        {
            I = new LocalizationManager();
        }

        LocalizationManager()
        {
            LoadLocale(_localeKey.Value);
        }

        private void LoadLocale(string localeToLoad)
        {
            _localeDef = Resources.Load<LocalDef>($"Locales/{localeToLoad}");
            OnLocaleChanged?.Invoke();
        }

        public string Locolize(string key)
        {
            return null;
        }
    }
}