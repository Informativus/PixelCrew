using System;
using PixelCrew.Model.Definitions.Localization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PixelCrew.UI.Widgets
{
    public class LocaleItemWidget : MonoBehaviour, IItemRenderer<LocaleInfo>
    {
        [SerializeField] private Text _text;
        [SerializeField] private SelectedLocale _onSelected;

        private LocaleInfo _data;

        private void Start()
        {
            LocalizationManager.I.OnLocaleChanged += UpdateSelection;
        }

        public void SetData(LocaleInfo localeInfo, int index)
        {
            _data = localeInfo;
            _text.text = localeInfo.LocaleId.ToUpper();
        }

        private void UpdateSelection()
        {
        }

        public void OnSelected()
        {
            _onSelected?.Invoke(_data.LocaleId);
        }
    }

    [Serializable]
    public class SelectedLocale : UnityEvent<string>
    {
    }

    public class LocaleInfo
    {
        public string LocaleId;
    }
}