using System;
using System.Collections.Generic;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions.Localization;
using PixelCrew.UI.Widgets;
using UnityEngine;

namespace PixelCrew.UI.Windows.Settings
{
    public class SettingsWindow : AnimatedWindow
    {
        [SerializeField] private AudioSettingsWidget _music;
        [SerializeField] private AudioSettingsWidget _sfx;

        [SerializeField] private Transform _container;
        [SerializeField] private LocaleItemWidget _prefab;

        private DataGroup<LocaleInfo, LocaleItemWidget> _dataGroup;

        private readonly string[] _supportedLocales = { "en", "ru" };

        protected override void OnEnable()
        {
            base.OnEnable();
            _music.SetModel(GameSettings.I.Music);
            _sfx.SetModel(GameSettings.I.Sfx);

            _dataGroup = new DataGroup<LocaleInfo, LocaleItemWidget>(_prefab, _container);

            _dataGroup.SetData(ComposeData());
        }

        private List<LocaleInfo> ComposeData()
        {
            var data = new List<LocaleInfo>();
            foreach (var locale in _supportedLocales)
            {
                data.Add(new LocaleInfo { LocaleId = locale });
            }

            return data;
        }

        public void OnSelected(string selectedLocale)
        {
            LocalizationManager.I.SetLocale(selectedLocale);
        }
    }
}