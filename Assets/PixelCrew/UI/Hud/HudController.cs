using System;
using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using PixelCrew.UI.Widgets;
using UnityEngine;
using UnityEngine.Serialization;

namespace PixelCrew.UI.Hud
{
    public class HudController : MonoBehaviour
    {
        [FormerlySerializedAs("_healthBar")] [SerializeField] private ProgressBarWidget _HpPanel;
        
        private GameSession _session;
        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _session.Data.Hp.OnChanged += OnHealthChanged;
        }

        private void OnHealthChanged(int newValue, int oldValue)
        { 
            var maxHealth = DefsFacade.I.Player.MaxHealth;
            var value = (float)newValue / maxHealth;
            _HpPanel.SetProgress(value);
        }

        private void OnDestroy()
        {
            _session.Data.Hp.OnChanged -= OnHealthChanged;
        }
    }
}