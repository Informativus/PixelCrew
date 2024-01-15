
using System;
using Model.Data;
using PixelCrew.UI.Widgets;
using UnityEngine;

namespace PixelCrew.UI.Settings
{
    public class SettingsWindow : AnimatedWindow
    {
        [SerializeField] private AudioSettingsWidget _music;
        [SerializeField] private AudioSettingsWidget _sfx;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            _music.SetModel(GameSettings.I.Music);
            _sfx.SetModel(GameSettings.I.Sfx);
            
        }
    }
}