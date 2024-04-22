using System;
using System.Collections.Generic;
using PixelCrew.Components.Dialogs;
using PixelCrew.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

namespace PixelCrew.Model.Definitions.Localization
{
    [CreateAssetMenu(menuName = "Defs/LocaleDef", fileName = "Locale")]
    public class LocalDef : ScriptableObject
    {
        [SerializeField] private string _url;
        [SerializeField] private List<LocaleItem> _localeItems;

        private UnityWebRequest _request;

        [ContextMenu("Update locale")]
        public void UpdateLocale()
        {
            if (_request != null) return;

            _request = UnityWebRequest.Get(_url);
            _request.SendWebRequest().completed += OnDataLoaded;
        }

        private void OnDataLoaded(AsyncOperation operation)
        {
            if (operation.isDone)
            {
                var json = _request.downloadHandler.text;
                
                var data = LocalizationHelper.FromJson(json);
                AddInLocaleItem(data);

            }
        }


        private void AddInLocaleItem(Dictionary<string, string> data)
        {
            try
            {
                foreach (var pair in data)
                {
                    var item = new LocaleItem
                    {
                        Key = pair.Key,
                        Value = pair.Value
                    };

                    _localeItems.Add(item);
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Can't parse data: {data}. \n {e}");
            }
        }

        [Serializable]
        private class LocaleItem
        {
            public string Key;
            public string Value;
        }
    }
}